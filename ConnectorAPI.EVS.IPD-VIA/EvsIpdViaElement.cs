namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA
{
    using Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Messages;
    using Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Model;
    using Skyline.DataMiner.Core.DataMinerSystem.Common;
    using Skyline.DataMiner.Core.InterAppCalls.Common.CallBulk;
    using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;
    using Skyline.DataMiner.Core.InterAppCalls.Common.Shared;
    using Skyline.DataMiner.Net;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an EVS IPD VIA element in DataMiner.
    /// </summary>
    public class EvsIpdViaElement
    {
        /// <summary>
        /// ID of the parameter in the EVS IPD VIA protocol that's used to receive incoming InterApp messages.
        /// </summary>
        public const int InterAppReceive_ParameterId = 9000000;

        private readonly IConnection connection;
        private readonly IDmsElement element;

        private IDictionary<string, object[]> targetsTable;
        private IDictionary<string, object[]> recodersTable;
        private TimeSpan? timeout;
        private ILogger logObject;

        private static readonly List<Type> knownTypes = new List<Type>
        {
            typeof(AddOrUpdateRecordingSession),
            typeof(AddOrUpdateRecordingSessionResult),
            typeof(DeleteRecordingSession),
            typeof(RecordingSession),
            typeof(Metadata),
            typeof(List<Metadata>),
            typeof(Metadata[]),
            typeof(Dictionary<string, string>),
            typeof(ReturnAddress)
        };

        /// <summary>
        /// List of known types. Used during InterApp communication.
        /// </summary>
        public static IEnumerable<Type> KnownTypes => knownTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="EvsIpdViaElement"/> class.
        /// </summary>
        /// <param name="connection">Connection used to communicate with the EVS element.</param>
        /// <param name="agentId">ID of the agent on which the EVS element is hosted.</param>
        /// <param name="elementId">ID of the EVS element.</param>
        /// <exception cref="ArgumentNullException">Thrown when the provided connection or the element is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when described element is inactive.</exception>
        public EvsIpdViaElement(IConnection connection, int agentId, int elementId, ILogger logObject)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
            element = connection.GetDms().GetElement(new DmsElementId(agentId, elementId));
            if (element.State != ElementState.Active) throw new InvalidOperationException($"Element {element.Name} is not active");
        }

        /// <summary>
        /// Gets the name of the DataMiner element.
        /// </summary>
        public string Name => element.Name;

        /// <summary>
        /// Maximum amount of time in which every request to the EVS element should be handled.
        /// Default: 30 seconds.
        /// </summary>
        public TimeSpan Timeout
        {
            get
            {
                if (timeout != null) return (TimeSpan)timeout;
                var timeoutInSeconds = element.GetStandaloneParameter<double?>(EvsIpdViaProtocol.InterAppTimeout).GetValue();
                logObject.Log(nameof(EvsIpdViaElement), nameof(Timeout), $"Timeout in seconds: {timeoutInSeconds}");
                timeout = TimeSpan.FromSeconds((double)timeoutInSeconds);
                logObject.Log(nameof(EvsIpdViaElement), nameof(Timeout), $"Timeout in timespan: {timeout}");
                return (TimeSpan)timeout;
            }
        }

        /// <summary>
        /// Adds or updates a recording session in the EVS system.
        /// If the ID of the recording session does not exist in EVS, a new recording session is created, else the existing one is updated.
        /// This method uses InterApp to forward the recording session to EVS.
        /// </summary>
        /// <param name="recordingSession">Recording session to add or update.</param>
        /// <returns>Updated recording session from EVS.</returns>
        /// <exception cref="ArgumentNullException">If recording session is null.</exception>
        /// <exception cref="InvalidOperationException">If InterApp communication fails.</exception>
        public RecordingSession AddOrUpdateRecordingSession(RecordingSession recordingSession)
        {
            if (recordingSession == null) throw new ArgumentNullException(nameof(recordingSession));

            var message = new AddOrUpdateRecordingSession
            {
                RecordingSession = recordingSession
            };

            if (!TrySendMessage(message, true, out string reason, out AddOrUpdateRecordingSessionResult result))
            {
                throw new InvalidOperationException($"Unable to add or update recording session with id {recordingSession.Id} due to {reason}");
            }

            if (!result.Success)
            {
                throw new InvalidOperationException($"Unable to add or update recording session with id {recordingSession.Id} due to {result.ErrorMessage}");
            }

            return result.RecordingSession;
        }

        /// <summary>
        /// Deletes a recording session from the EVS system.
        /// This method uses InterApp to delete the recording session from EVS.
        /// </summary>
        /// <param name="recordingSessionId">ID of the recording session to remove.</param>
        /// <exception cref="ArgumentException">If recording session id is null or whitespace.</exception>
        /// <exception cref="InvalidOperationException">If InterApp communication fails.</exception>
        public void DeleteRecordingSession(string recordingSessionId)
        {
            if (String.IsNullOrWhiteSpace(recordingSessionId)) throw new ArgumentException(nameof(recordingSessionId));

            var message = new DeleteRecordingSession
            {
                RecordingSessionsId = recordingSessionId
            };

            if (!TrySendMessage(message, false, out string reason, out Message _))
            {
                throw new InvalidOperationException($"Unable to delete recording session with id {recordingSessionId} due to {reason}");
            }
        }

        /// <summary>
        /// Gets the available recorders from the Recorders table from the element.
        /// </summary>
        /// <returns>List of available recorders.</returns>
        public List<Recorder> GetRecorderNames()
        {
            recodersTable = recodersTable ?? element.GetTable(EvsIpdViaProtocol.RecordersTable.TablePid)?.GetData();
            return recodersTable.Select(x => new Recorder { Instance = x.Key, Name = Convert.ToString(x.Value[EvsIpdViaProtocol.RecordersTable.Idx.RecordersName]) }).ToList();
        }

        /// <summary>
        /// Gets the names of the available targets from the Targets table from the element.
        /// </summary>
        /// <returns>List of target names.</returns>
        public IEnumerable<string> GetTargetNames()
        {
            targetsTable = targetsTable ?? element.GetTable(EvsIpdViaProtocol.TargetsTable.TablePid).GetData();
            return targetsTable.Values.Select(x => Convert.ToString(x[EvsIpdViaProtocol.TargetsTable.Idx.TargetsName])).ToList();
        }

        /// <summary>
        /// Retrieves the metadata labels from the Profile Fields table.
        /// </summary>
        /// <returns>All labels from the Profile Fields table.</returns>
        public List<Label> GetMetadataLabels()
        {
            var profileFieldsTable = element.GetTable(EvsIpdViaProtocol.ProfileFieldsTable.TablePid);

            return profileFieldsTable.GetData().Values.Select(x => new Label
            {
                Key = Convert.ToString(x[EvsIpdViaProtocol.ProfileFieldsTable.Idx.ProfileFieldsKey]),
                Name = Convert.ToString(x[EvsIpdViaProtocol.ProfileFieldsTable.Idx.ProfileFieldsLabel]),
                Type = Convert.ToString(x[EvsIpdViaProtocol.ProfileFieldsTable.Idx.ProfileFieldsType]),
                Required = Convert.ToBoolean(x[EvsIpdViaProtocol.ProfileFieldsTable.Idx.ProfileFieldsRequired]),
                ProfileFqn = Convert.ToString(x[EvsIpdViaProtocol.ProfileFieldsTable.Idx.ProfileFieldsProfileFqn]),
                ProfileName = Convert.ToString(x[EvsIpdViaProtocol.ProfileFieldsTable.Idx.ProfileFieldsProfileName])
            }).ToList();
        }

        /// <summary>
        /// Retrieves a recording session from the EVS element.
        /// All data is retrieved from the tables available in the element.
        /// </summary>
        /// <param name="recordingSessionId">ID of the recording session to retrieve.</param>
        /// <returns>Recording session with given ID.</returns>
        /// <exception cref="ArgumentException">If recording session ID is null or whitespace.</exception>
        public RecordingSession GetRecordingSession(string recordingSessionId)
        {
            if (String.IsNullOrWhiteSpace(recordingSessionId)) throw new ArgumentException(nameof(recordingSessionId));

            var recordingSessionsTable = element.GetTable(EvsIpdViaProtocol.RecordingSessionsTable.TablePid);
            var row = recordingSessionsTable.GetRow(recordingSessionId);

            RecordingSession recordingSession = new RecordingSession
            {
                Id = Convert.ToString(row[EvsIpdViaProtocol.RecordingSessionsTable.Idx.RecordingSessionsInstanceIdx]),
                Name = Convert.ToString(row[EvsIpdViaProtocol.RecordingSessionsTable.Idx.RecordingSessionsNameIdx]),
                Start = DateTime.FromOADate(Convert.ToDouble(row[EvsIpdViaProtocol.RecordingSessionsTable.Idx.RecordingSessionsStartIdx])),
                End = DateTime.FromOADate(Convert.ToDouble(row[EvsIpdViaProtocol.RecordingSessionsTable.Idx.RecordingSessionsEndIdx])),
                Recorder = Convert.ToString(row[EvsIpdViaProtocol.RecordingSessionsTable.Idx.RecordingSessionsRecorderIdx])
            };

            // Get Targets
            var recordingSessionsTargetsTable = element.GetTable(EvsIpdViaProtocol.RecordingSessionsTargetsTable.TablePid);
            recordingSession.Targets = recordingSessionsTargetsTable.QueryData(new[]
            {
                new ColumnFilter
                {
                    Pid = EvsIpdViaProtocol.RecordingSessionsTargetsTable.Pid.RecordingSessionsTargetsRecordingSessionInstance,
                    ComparisonOperator = ComparisonOperator.Equal,
                    Value = recordingSessionId
                }
            }).Select(x => Convert.ToString(x[EvsIpdViaProtocol.RecordingSessionsTargetsTable.Idx.RecordingSessionsTargetsTarget])).ToArray();

            // Get MetaData
            var metaDataTable = element.GetTable(EvsIpdViaProtocol.RecordingSessionsMetadataValuesTable.TablePid);
            var metaDataEntries = metaDataTable.QueryData(new[]
            {
                new ColumnFilter
                {
                    Pid = EvsIpdViaProtocol.RecordingSessionsMetadataValuesTable.Pid.RecordingSessionsMetadataValuesRecordingSessionId,
                    ComparisonOperator = ComparisonOperator.Equal,
                    Value = recordingSessionId
                }
            });

            var profileFieldsTableData = element.GetTable(EvsIpdViaProtocol.ProfileFieldsTable.TablePid).GetData();
            Dictionary<string, Metadata> metadataToStore = new Dictionary<string, Metadata>();
            foreach (var metaDataEntry in metaDataEntries)
            {
                string profileFqn = Convert.ToString(metaDataEntry[EvsIpdViaProtocol.RecordingSessionsMetadataValuesTable.Idx.RecordingSessionsMetadataValuesProfile]);
                string label = Convert.ToString(metaDataEntry[EvsIpdViaProtocol.RecordingSessionsMetadataValuesTable.Idx.RecordingSessionsMetadataValuesKey]);
                string value = Convert.ToString(metaDataEntry[EvsIpdViaProtocol.RecordingSessionsMetadataValuesTable.Idx.RecordingSessionsMetadataValuesValue]);

                string key = GetProfileFieldKey(profileFqn, label, profileFieldsTableData.Values);
                if (String.IsNullOrWhiteSpace(key)) continue;

                if (metadataToStore.TryGetValue(profileFqn, out Metadata metadata))
                {
                    metadata.Values[key] = value;
                }
                else
                {
                    metadataToStore.Add(profileFqn, new Metadata
                    {
                        Profile = profileFqn,
                        Values = new Dictionary<string, string>
                        {
                            { key, value }
                        }
                    });
                }
            }

            recordingSession.Metadata = metadataToStore.Values.ToList();

            return recordingSession;
        }

        private static string GetProfileFieldKey(string profileFqn, string profileFieldLabel, IEnumerable<object[]> profileFieldsRows)
        {
            foreach (var row in profileFieldsRows)
            {
                string rowProfileFqn = Convert.ToString(row[EvsIpdViaProtocol.ProfileFieldsTable.Idx.ProfileFieldsProfileFqn]);
                string rowProfileFieldLabel = Convert.ToString(row[EvsIpdViaProtocol.ProfileFieldsTable.Idx.ProfileFieldsLabel]);

                if (String.Equals(profileFqn, rowProfileFqn, StringComparison.InvariantCultureIgnoreCase) && String.Equals(profileFieldLabel, rowProfileFieldLabel, StringComparison.InvariantCultureIgnoreCase))
                {
                    return Convert.ToString(row[EvsIpdViaProtocol.ProfileFieldsTable.Idx.ProfileFieldsKey]);
                }
            }

            return String.Empty;
        }

        private bool TrySendMessage<T>(Message message, bool requiresResponse, out string reason, out T responseMessage) where T : Message
        {
            reason = String.Empty;
            responseMessage = default(T);

            var commands = InterAppCallFactory.CreateNew();
            commands.Messages.Add(message);

            try
            {
                if (requiresResponse)
                {
                    var response = commands.Send(connection, element.AgentId, element.Id, InterAppReceive_ParameterId, Timeout, knownTypes).First();
                    if (!(response is T castResponse))
                    {
                        reason = $"Received response is not of type {typeof(T)}";
                        return false;
                    }

                    responseMessage = castResponse;
                }
                else
                {
                    commands.Send(connection, element.AgentId, element.Id, InterAppReceive_ParameterId, knownTypes);
                }
            }
            catch (Exception e)
            {
                if (requiresResponse)
                {
                    throw new InvalidOperationException($"Configured Timeout {timeout}s", e);
                }
                else
                {
                    reason = e.ToString();
                    return false;
                }
            }

            return true;
        }

        public void Log(string nameOfClass, string nameOfMethod, string message)
        {
            
        }
    }
}

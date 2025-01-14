namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA
{
    using Newtonsoft.Json;
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
	public class EvsIpdViaElement : IEvsIpdViaElement
	{
		/// <summary>
		/// ID of the parameter in the EVS IPD VIA protocol that's used to receive incoming InterApp messages.
		/// </summary>
		public const int InterAppReceive_ParameterId = 9000000;

		private readonly IConnection connection;
		private readonly IDmsElement element;
		private readonly ILogger logger;

		private TimeSpan? timeout;

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
		/// <param name="logger">Object that inherits ILogger interface used to log info if needed.</param>
		/// <exception cref="ArgumentNullException">Thrown when the provided connection or the element is null.</exception>
		/// <exception cref="InvalidOperationException">Thrown when described element is inactive.</exception>
		public EvsIpdViaElement(IConnection connection, int agentId, int elementId, ILogger logger = null)
		{
			this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
			element = connection.GetDms().GetElement(new DmsElementId(agentId, elementId));
			this.logger = logger;
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
				try
				{
					var timeoutInSeconds = element.GetStandaloneParameter<double?>(EvsIpdViaProtocol.InterAppTimeout) ?? throw new NullReferenceException("InterApp Timeout value is null.");
					timeout = TimeSpan.FromSeconds(timeoutInSeconds.GetValue().Value);
					Log(nameof(EvsIpdViaElement), nameof(Timeout), $"Timeout timespan: {timeout}");
					return (TimeSpan)timeout;
				}
				catch (Exception e)
				{
					timeout = TimeSpan.FromSeconds(30);
					Log(nameof(EvsIpdViaElement), nameof(Timeout), $"Unable to retrieve timeout due to: {e}");
					return (TimeSpan)timeout;
				}
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

		private bool TrySendMessage<T>(Message message, bool requiresResponse, out string reason, out T responseMessage) where T : Message
		{
			reason = String.Empty;
			responseMessage = default(T);

			var commands = InterAppCallFactory.CreateNew();
			commands.Messages.Add(message);

			Log(nameof(EvsIpdViaElement), nameof(TrySendMessage), $"Message: {JsonConvert.SerializeObject(message)}");

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

					Log(nameof(EvsIpdViaElement), nameof(TrySendMessage), $"Response: {JsonConvert.SerializeObject(response)}");
					responseMessage = castResponse;
				}
				else
				{
					commands.Send(connection, element.AgentId, element.Id, InterAppReceive_ParameterId, knownTypes);
				}
			}
			catch (Exception e)
			{
				reason = e.ToString();
				return false;
			}

			return true;
		}

		/// <summary>
		/// Log info that will be used for debug. If logObject is null, log method won't provide any logging.
		/// </summary>
		/// <param name="nameOfClass">Name of class log info is coming from.</param>
		/// <param name="nameOfMethod">Name of method log info is coming from.</param>
		/// <param name="message">Message that will be logged.</param>
		private void Log(string nameOfClass, string nameOfMethod, string message)
		{
			if (logger == null) return;
			logger.Log(nameOfClass, nameOfMethod, message);
		}
	}
}

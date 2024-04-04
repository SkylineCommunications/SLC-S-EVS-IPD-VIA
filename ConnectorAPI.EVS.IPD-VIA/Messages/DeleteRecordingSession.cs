using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;

namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Messages
{
    /// <summary>
    /// Represents a message to delete a recording session.
    /// </summary>
    public class DeleteRecordingSession : Message
    {
        /// <summary>
        /// Gets or sets the ID of the recording session to delete.
        /// </summary>
        public string RecordingSessionsId { get; set; }
    }
}
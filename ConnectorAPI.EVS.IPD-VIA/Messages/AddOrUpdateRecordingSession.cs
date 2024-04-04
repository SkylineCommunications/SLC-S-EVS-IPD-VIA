using Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Model;
using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;

namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Messages
{
    /// <summary>
    /// Represents a message to add or update a recording session.
    /// </summary>
    public class AddOrUpdateRecordingSession : Message
    {
        /// <summary>
        /// Gets or sets the recording session to add or update.
        /// </summary>
        public RecordingSession RecordingSession { get; set; }
    }
}

namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Messages
{
    using Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Model;

    /// <summary>
    /// Represents the result of adding or updating a recording session.
    /// </summary>
    public class AddOrUpdateRecordingSessionResult : Result
    {
        /// <summary>
        /// Gets or sets the recording session that was added or updated.
        /// </summary>
        public RecordingSession RecordingSession { get; set; }
    }
}

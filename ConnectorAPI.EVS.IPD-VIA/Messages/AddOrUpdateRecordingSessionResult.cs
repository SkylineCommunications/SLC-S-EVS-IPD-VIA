namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Messages
{
    using Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Model;

    public class AddOrUpdateRecordingSessionResult : Result
    {
        public RecordingSession RecordingSession { get; set; }
    }
}
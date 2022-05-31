namespace Skyline.DataMiner.EVS.EVS_IPD_VIA.Messages
{
    using Skyline.DataMiner.EVS.EVS_IPD_VIA.Model;

    public class AddOrUpdateRecordingSessionResult : Result
    {
        public RecordingSession RecordingSession { get; set; }
    }
}
namespace Skyline.DataMiner.EVS.EVS_IPD_VIA.Messages
{
    using Skyline.DataMiner.EVS.EVS_IPD_VIA.Model;

    public class AddOrUpdateRecordingSessionResponseMessage : ResponseMessage
    {
        public RecordingSession RecordingSession { get; set; }
    }
}
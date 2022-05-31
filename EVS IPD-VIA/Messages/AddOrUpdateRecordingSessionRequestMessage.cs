namespace Skyline.DataMiner.EVS.EVS_IPD_VIA.Messages
{
    using Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle;
    using Skyline.DataMiner.EVS.EVS_IPD_VIA.Model;

    public class AddOrUpdateRecordingSessionRequestMessage : Message
    {
        public RecordingSession RecordingSession { get; set; }
    }
}
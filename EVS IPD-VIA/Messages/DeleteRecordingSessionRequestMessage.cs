namespace Skyline.DataMiner.EVS.EVS_IPD_VIA.Messages
{
    using Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle;

    public  class DeleteRecordingSessionRequestMessage : Message
    {
        public string RecordingSessionsId { get; set; }
    }
}
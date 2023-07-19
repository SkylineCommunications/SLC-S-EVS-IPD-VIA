namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Messages
{
    using Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle;

    public  class DeleteRecordingSession : Message
    {
        public string RecordingSessionsId { get; set; }
    }
}
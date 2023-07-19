namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Messages
{
    using Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle;
    using Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Model;

    public class AddOrUpdateRecordingSession : Message
    {
        public RecordingSession RecordingSession { get; set; }
    }
}
namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Messages
{
	using Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Model;
	using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;

	public class AddOrUpdateRecordingSession : Message
    {
        public RecordingSession RecordingSession { get; set; }
    }
}
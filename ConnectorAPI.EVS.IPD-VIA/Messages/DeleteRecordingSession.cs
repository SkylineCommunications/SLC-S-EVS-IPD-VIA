namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Messages
{
	using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;

	public  class DeleteRecordingSession : Message
    {
        public string RecordingSessionsId { get; set; }
    }
}
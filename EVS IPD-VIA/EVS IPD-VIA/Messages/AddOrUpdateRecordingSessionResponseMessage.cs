using Skyline.DataMiner.Library.EVS.EVS_IPD_VIA.Model;

namespace Skyline.DataMiner.Library.EVS.EVS_IPD_VIA.Messages
{
    public class AddOrUpdateRecordingSessionResponseMessage : ResponseMessage
    {
        public RecordingSession RecordingSession { get; set; }
    }
}
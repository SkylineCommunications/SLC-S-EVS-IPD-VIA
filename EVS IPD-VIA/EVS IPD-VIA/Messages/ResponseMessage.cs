namespace Skyline.DataMiner.Library.EVS.EVS_IPD_VIA.Messages
{
    using Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle;

    public class ResponseMessage : Message
    {
        public bool IsSuccessful { get; set; }

        public string ErrorMessage { get; set; }
    }
}
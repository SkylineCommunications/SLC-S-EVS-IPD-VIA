namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Messages
{
    using Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle;

    public class Result : Message
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
    }
}
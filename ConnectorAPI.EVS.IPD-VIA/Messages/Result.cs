namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Messages
{
    using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;

    public class Result : Message
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
    }
}
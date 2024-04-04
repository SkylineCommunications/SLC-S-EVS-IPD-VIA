using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;

namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Messages
{
    /// <summary>
    /// Represents a result message.
    /// </summary>
    public class Result : Message
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the error message if the operation was not successful.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
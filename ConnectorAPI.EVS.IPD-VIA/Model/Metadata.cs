using System.Collections.Generic;

namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Model
{
    /// <summary>
    /// Represents metadata in the EVS IPD-VIA model.
    /// </summary>
    public class Metadata
    {
        /// <summary>
        /// Gets or sets the profile associated with the metadata.
        /// </summary>
        public string Profile { get; set; }

        /// <summary>
        /// Gets or sets the dictionary containing metadata values. Key represents the metadata key, and value represents the metadata value.
        /// </summary>
        public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
    }
}
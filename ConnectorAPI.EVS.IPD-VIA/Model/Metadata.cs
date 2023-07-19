using System.Collections.Generic;

namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Model
{
    public class Metadata
    {
        public string Profile { get; set; }

        public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
    }
}

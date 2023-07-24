namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Model
{
	using System.Collections.Generic;

	public class Metadata
	{
		public string Profile { get; set; }

		public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
	}
}

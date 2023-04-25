namespace Skyline.DataMiner.EVS.EVS_IPD_VIA.Model
{
    using System;
	using System.Collections.Generic;

	public class RecordingSession
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Recorder { get; set; }

        public IEnumerable<string> Targets { get; set; } = new List<string>();

        public IEnumerable<Metadata> Metadata { get; set; } = new List<Metadata>();
    }
}
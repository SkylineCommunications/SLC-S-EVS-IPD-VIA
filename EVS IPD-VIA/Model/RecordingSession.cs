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

        public string[] Targets { get; set; }

        public Metadata metadata { get; set; }
    }

    public class Metadata
    {
        string[] profilesFqn { get; set; }

        Dictionary<string, string> values { get; set; }
    }
}
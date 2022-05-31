namespace Skyline.DataMiner.EVS.EVS_IPD_VIA.Model
{
    using System;

    public class RecordingSession
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Recorder { get; set; }

        public string[] Targets { get; set; }
    }
}
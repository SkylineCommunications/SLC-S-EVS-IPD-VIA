using System;
using System.Collections.Generic;

namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Model
{
    /// <summary>
    /// Represents a recording session in the EVS IPD-VIA model.
    /// </summary>
    public class RecordingSession
    {
        /// <summary>
        /// Gets or sets the ID of the recording session.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the recording session.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the start time of the recording session.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Gets or sets the end time of the recording session.
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// Gets or sets the recorder associated with the recording session.
        /// </summary>
        public string Recorder { get; set; }

        /// <summary>
        /// Gets or sets the list of targets associated with the recording session.
        /// </summary>
        public IEnumerable<string> Targets { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the metadata associated with the recording session.
        /// </summary>
        public IEnumerable<Metadata> Metadata { get; set; } = new List<Metadata>();
    }
}
namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Model
{
    /// <summary>
    /// Represents a label in the EVS IPD-VIA model.
    /// </summary>
    public class Label
    {
        /// <summary>
        /// Gets or sets the name of the label.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the profile name associated with the label.
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets the key of the label.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the fully qualified name of the profile associated with the label.
        /// </summary>
        public string ProfileFqn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the label is required.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets the type of the label.
        /// </summary>
        public string Type { get; set; }
    }
}
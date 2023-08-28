namespace Chart.Language
{
    public enum SchemaParserIgnore
    {
        /// <summary>
        /// Don't ignore any nodes.
        /// </summary>
        None,

        /// <summary>
        /// Ignore descriptions.
        /// </summary>
        Descriptions,

        /// <summary>
        /// Ignore all non-essential nodes.
        /// </summary>
        All
    }

    public class SchemaParserOptions
    {
        /// <summary>
        /// The maximum quering depth to avoid long response times and petty DDoS-attacks.
        /// </summary>
        public int MaxDepth { get; set; } = 128;

        /// <summary>
        /// Select which nodes to ignore in the document.
        /// </summary>
        public SchemaParserIgnore Ignore { get; set; } = SchemaParserIgnore.None;
    }
}
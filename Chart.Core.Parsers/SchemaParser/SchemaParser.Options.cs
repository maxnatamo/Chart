namespace Chart.Core.Parsers
{
    public class SchemaParserOptions
    {
        /// <summary>
        /// The maximum quering depth to avoid long response times and petty DDoS-attacks.
        /// </summary>
        public int MaxDepth { get; set; } = 128;
    }
}
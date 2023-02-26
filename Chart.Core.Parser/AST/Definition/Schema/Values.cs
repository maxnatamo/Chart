namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of schema values in a schema definition.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#SchemaDefinition">Original documentation</seealso>
    public class GraphSchemaValues : GraphNode
    {
        /// <summary>
        /// List of nodes in the enum.
        /// </summary>
        public List<GraphSchemaValue> Values { get; set; } = default!;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphSchemaValues]";
        }
    }
}
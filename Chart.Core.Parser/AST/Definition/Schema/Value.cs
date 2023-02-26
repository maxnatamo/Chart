namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a schema value in a schema definition.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#RootOperationTypeDefinition">Original documentation</seealso>
    public class GraphSchemaValue : GraphNode
    {
        /// <summary>
        /// Name of the selected operation.
        /// </summary>
        public GraphName Operation { get; set; } = default!;

        /// <summary>
        /// The type bound to the operation in the schema.
        /// </summary>
        public GraphNamedType Type { get; set; } = default!;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphSchemaValue]";
        }
    }
}
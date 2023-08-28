namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of schema values in a schema definition.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#SchemaDefinition">Original documentation</seealso>
    public class GraphSchemaValues : IGraphNode
    {
        /// <summary>
        /// List of nodes in the enum.
        /// </summary>
        public List<GraphSchemaValue> Values { get; set; } = default!;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => $"[GraphSchemaValues]";
    }
}
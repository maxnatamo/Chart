namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a field-list in the GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#FieldsDefinition">Original documentation</seealso>
    public class GraphFields : IGraphNode
    {
        /// <summary>
        /// The underlying list of field definitions.
        /// </summary>
        public List<GraphField> Fields { get; set; } = new List<GraphField>();

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => "[GraphFields]";
    }
}
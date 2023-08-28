namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a field in the GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#FieldDefinition">Original documentation</seealso>
    public class GraphField : IGraphNode, IHasDirectives, IHasDescription, IHasName
    {
        /// <summary>
        /// The name for the field.
        /// </summary>
        public GraphName Name { get; set; } = default!;

        /// <summary>
        /// The type of the field.
        /// </summary>
        public GraphType Type { get; set; } = default!;

        /// <summary>
        /// Optional description for the selection.
        /// </summary>
        public GraphDescription? Description { get; set; } = null;

        /// <summary>
        /// Optional variables for the selection.
        /// </summary>
        public GraphArgumentsDefinition? Arguments { get; set; } = null;

        /// <summary>
        /// Optional directives for the selection.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => $"[GraphField]";
    }
}
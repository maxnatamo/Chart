namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of an enum value in an enum definition.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#EnumValueDefinition">Original documentation</seealso>
    public class GraphEnumDefinitionValue : IGraphNode, IHasDescription, IHasName
    {
        /// <summary>
        /// List of nodes in the enum.
        /// </summary>
        public GraphName Name { get; set; } = default!;

        /// <summary>
        /// The optional description for the enum value.
        /// </summary>
        public GraphDescription? Description { get; set; } = null;

        /// <summary>
        /// Optional directives of the enum.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => $"[GraphEnumDefinitionValue]";
    }
}
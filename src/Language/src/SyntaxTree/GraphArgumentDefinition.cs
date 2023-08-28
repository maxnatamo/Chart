namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of an argument in an argument list.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#InputValueDefinition">Original documentation</seealso>
    public class GraphArgumentDefinition : IGraphNode, IHasDirectives, IHasDescription, IHasName
    {
        /// <summary>
        /// The name of the variable.
        /// </summary>
        public GraphName Name { get; set; } = default!;

        /// <summary>
        /// An optional description of the argument.
        /// </summary>
        public GraphDescription? Description { get; set; } = null;

        /// <summary>
        /// Set the kind of argument.
        /// </summary>
        public GraphType Type { get; set; } = default!;

        /// <summary>
        /// The optional default value of the argument.
        /// </summary>
        public IGraphValue? DefaultValue { get; set; } = null;

        /// <summary>
        /// The optional directives of the argument.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => "[GraphArgumentDefinition]";
    }
}
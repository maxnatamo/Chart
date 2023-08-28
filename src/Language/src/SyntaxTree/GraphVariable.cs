namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a variable in a variable list.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#VariableDefinition">Original documentation</seealso>
    public class GraphVariable : IGraphNode, IHasDirectives, IHasName
    {
        /// <summary>
        /// The name of the variable.
        /// </summary>
        public GraphName Name { get; set; } = default!;

        /// <summary>
        /// The type of the variable.
        /// </summary>
        public GraphType Type { get; set; } = default!;

        /// <summary>
        /// The default value of the variable.
        /// </summary>
        public IGraphValue? DefaultValue { get; set; } = null;

        /// <summary>
        /// Optional directives for the variable.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => $"[GraphVariableDefinition]";
    }
}
namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a single selection in a selection-list
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Selection">Original documentation</seealso>
    public abstract class GraphSelection : IGraphNode, IHasDirectives
    {
        /// <summary>
        /// The type of selection.
        /// </summary>
        public abstract GraphSelectionKind SelectionKind { get; }

        /// <summary>
        /// Optional arguments to the selection.
        /// </summary>
        public GraphArguments? Arguments { get; set; } = null;

        /// <summary>
        /// Optional directives for the selection.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public abstract override string ToString();
    }
}
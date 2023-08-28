namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a selection-list in the GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#sec-Selection-Sets">Original documentation</seealso>
    public class GraphSelectionSet : IGraphNode
    {
        /// <summary>
        /// The underlying list of selections.
        /// </summary>
        public List<GraphSelection> Selections { get; set; } = new List<GraphSelection>();

        /// <summary>
        /// Optional arguments to the selection.
        /// </summary>
        public GraphArguments? Arguments { get; set; } = null;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => $"[GraphSelectionSet]";
    }
}
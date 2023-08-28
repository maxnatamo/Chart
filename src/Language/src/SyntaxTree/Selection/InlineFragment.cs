namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of an inline fragment in a selection.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#InlineFragment">Original documentation</seealso>
    public class GraphInlineFragmentSelection : GraphSelection
    {
        /// <inheritdoc />
        public override GraphSelectionKind SelectionKind => GraphSelectionKind.InlineFragment;

        /// <summary>
        /// The type attached to the type condition.
        /// </summary>
        public GraphNamedType? TypeCondition { get; set; } = null;

        /// <summary>
        /// The selection-set for the fragment.
        /// </summary>
        public GraphSelectionSet SelectionSet { get; set; } = default!;

        /// <inheritdoc />
        public override string ToString() => $"[GraphInlineFragmentSelection]";
    }
}
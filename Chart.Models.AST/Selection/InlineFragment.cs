namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of an inline fragment in a selection.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#InlineFragment">Original documentation</seealso>
    public class GraphInlineFragmentSelection : GraphSelection
    {
        /// <summary>
        /// Set the type of the selection to Field.
        /// </summary>
        public override GraphSelectionKind SelectionKind => GraphSelectionKind.InlineFragment;

        /// <summary>
        /// The type attached to the type condition.
        /// </summary>
        public GraphNamedType? TypeCondition { get; set; } = null;

        /// <summary>
        /// The selection-set for the fragment.
        /// </summary>
        public GraphSelectionSet SelectionSet { get; set; } = default!;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphInlineFragmentSelection]";
        }
    }
}
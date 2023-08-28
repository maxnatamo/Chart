namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a fragment selection in a selection.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#FragmentSpread">Original documentation</seealso>
    public class GraphFragmentSelection : GraphSelection, IHasName
    {
        /// <inheritdoc />
        public override GraphSelectionKind SelectionKind => GraphSelectionKind.Fragment;

        /// <summary>
        /// The name of the fragment.
        /// </summary>
        public GraphName Name { get; set; } = default!;

        /// <inheritdoc />
        public override string ToString() => $"[GraphFragmentSelection]";
    }
}
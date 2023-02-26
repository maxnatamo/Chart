namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a single field in a selection.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Field">Original documentation</seealso>
    public class GraphFragmentSelection : GraphSelection
    {
        /// <summary>
        /// Set the type of the selection to Field.
        /// </summary>
        public override GraphSelectionKind SelectionKind => GraphSelectionKind.Fragment;

        /// <summary>
        /// The name for the field.
        /// </summary>
        public GraphName Name { get; set; } = default!;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphFragmentSelection]";
        }
    }
}
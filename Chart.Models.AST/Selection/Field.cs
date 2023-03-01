namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a single field in a selection.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Field">Original documentation</seealso>
    public class GraphFieldSelection : GraphSelection
    {
        /// <summary>
        /// Set the type of the selection to Field.
        /// </summary>
        public override GraphSelectionKind SelectionKind => GraphSelectionKind.Field;

        /// <summary>
        /// Optional alias for the field.
        /// </summary>
        public GraphName? Alias { get; set; } = null;

        /// <summary>
        /// The name for the field.
        /// </summary>
        public GraphName Name { get; set; } = default!;

        /// <summary>
        /// Optional selection-set for the field.
        /// </summary>
        public GraphSelectionSet? SelectionSet { get; set; } = null;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphFieldSelection]";
        }
    }
}
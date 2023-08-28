namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a single field in a selection.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Field">Original documentation</seealso>
    public class GraphFieldSelection : GraphSelection, IHasName
    {
        /// <inheritdoc />
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

        /// <inheritdoc />
        public override string ToString() => $"[GraphFieldSelection]";
    }
}
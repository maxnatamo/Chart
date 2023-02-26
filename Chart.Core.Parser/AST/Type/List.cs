namespace Chart.Core.Parser
{
    /// <summary>
    /// List of objects inside of a type-definition.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ListType">Original documentation</seealso>
    public class GraphListType : GraphType
    {
        /// <summary>
        /// Set the kind of type-field.
        /// </summary>
        public override GraphTypeKind TypeKind => GraphTypeKind.List;

        /// <summary>
        /// The underlying type of the list.
        /// </summary>
        public GraphType UnderlyingType { get; set; } = default!;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[{UnderlyingType.ToString()}]{(this.NonNullable ? "!" : "")}";
        }
    }
}
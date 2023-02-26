namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a type.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Type">Original documentation</seealso>
    public abstract class GraphType : GraphNode
    {
        /// <summary>
        /// Whether the kind of type is a list, named type, etc.
        /// </summary>
        public abstract GraphTypeKind TypeKind { get; }

        /// <summary>
        /// Whether the type is non-nullable.
        /// </summary>
        /// <remarks>
        /// In the GraphQL spec, non-nullable items are entirely separate, but this makes
        /// the parsing easier and minimizes unneeded complexity.
        /// </remarks>
        /// <seealso href="https://spec.graphql.org/October2021/#NonNullType">Original documentation</seealso>
        public bool NonNullable { get; set; } = false;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public abstract override string ToString();
    }
}
namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a type.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Type">Original documentation</seealso>
    public abstract class GraphType : IGraphNode, ICloneable
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

        /// <inheritdoc />
        public GraphLocation? Location { get; set; }

        /// <inheritdoc />
        public abstract override string ToString();

        /// <inheritdoc />
        public abstract object Clone();
    }
}
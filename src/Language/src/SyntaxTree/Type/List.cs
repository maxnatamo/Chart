namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// List of objects inside of a type-definition.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ListType">Original documentation</seealso>
    public class GraphListType : GraphType
    {
        /// <inheritdoc />
        public override GraphTypeKind TypeKind => GraphTypeKind.List;

        /// <summary>
        /// The underlying type of the list.
        /// </summary>
        public GraphType UnderlyingType { get; set; } = default!;

        /// <summary>
        /// Initialize a new GraphListType-object without a type specified.
        /// </summary>
        public GraphListType()
        { }

        /// <summary>
        /// Initialize a new GraphListType-object with the type specified.
        /// </summary>
        /// <param name="underlyingType">The underlying type of the list.</param>
        public GraphListType(GraphType underlyingType)
        {
            this.UnderlyingType = underlyingType;
        }

        /// <inheritdoc />
        public override string ToString() => $"[{this.UnderlyingType}]{(this.NonNullable ? "!" : "")}";

        /// <inheritdoc />
        public override object Clone() =>
            new GraphListType
            {
                NonNullable = this.NonNullable,
                UnderlyingType = this.UnderlyingType
            };
    }
}
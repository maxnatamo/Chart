namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Named type inside of a type-definition.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#NamedType">Original documentation</seealso>
    public class GraphNamedType : GraphType
    {
        /// <inheritdoc />
        public override GraphTypeKind TypeKind => GraphTypeKind.Named;

        /// <summary>
        /// The name of the type.
        /// </summary>
        public GraphName Name { get; set; } = default!;

        /// <summary>
        /// Initialize a new GraphNamedType without any type attached.
        /// </summary>
        public GraphNamedType()
        { }

        /// <summary>
        /// Initialize a new GraphNamedType with a type attached.
        /// </summary>
        public GraphNamedType(string name)
        {
            this.Name = new GraphName(name);
        }

        /// <summary>
        /// Initialize a new GraphNamedType with a type attached.
        /// </summary>
        public GraphNamedType(GraphName name)
        {
            this.Name = name;
        }

        /// <inheritdoc />
        public override string ToString() => $"{this.Name.Value}{(this.NonNullable ? "!" : "")}";
    }
}
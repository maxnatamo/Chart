namespace Chart.Models.AST
{
    /// <summary>
    /// Named type inside of a type-definition.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#NamedType">Original documentation</seealso>
    public class GraphNamedType : GraphType
    {
        /// <summary>
        /// Set the kind of type-field.
        /// </summary>
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

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"{this.Name.Value}{(this.NonNullable ? "!" : "")}";
        }
    }
}
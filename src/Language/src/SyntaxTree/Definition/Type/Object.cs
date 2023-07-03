namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a object-type in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ObjectTypeDefinition">Original documentation</seealso>
    public class GraphObjectType : GraphTypeDefinition
    {
        /// <inheritdoc />
        public override GraphTypeDefinitionKind TypeKind => GraphTypeDefinitionKind.Object;

        /// <summary>
        /// Underlying fields of the type definition.
        /// </summary>
        public GraphFields? Fields { get; set; } = null;

        /// <summary>
        /// Optional interface of the interface.
        /// </summary>
        public GraphInterfaces? Interface { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => $"[GraphType] Object";
    }
}
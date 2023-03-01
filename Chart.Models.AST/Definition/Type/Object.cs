namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a object-type in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ObjectTypeDefinition">Original documentation</seealso>
    public class GraphObjectType : GraphTypeDefinition
    {
        /// <summary>
        /// Set the type of the operation to Object.
        /// </summary>
        public override GraphTypeDefinitionKind TypeKind => GraphTypeDefinitionKind.Object;

        /// <summary>
        /// Underlying fields of the type definition.
        /// </summary>
        public GraphFields? Fields { get; set; } = null;

        /// <summary>
        /// Optional interface of the interface.
        /// </summary>
        public GraphInterfaces? Interface { get; set; } = null;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphType] Object";
        }
    }
}
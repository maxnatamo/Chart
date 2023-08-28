namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of an interface in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#InterfaceTypeDefinition">Original documentation</seealso>
    public class GraphInterfaceDefinition : GraphDefinition, IHasDirectives
    {
        /// <inheritdoc />
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Interface;

        /// <inheritdoc />
        public override bool Executable => false;

        /// <summary>
        /// Optional directives for the extension.
        /// </summary>
        public GraphDirectives? Directives { get; set; }

        /// <summary>
        /// Optional fields in the interface.
        /// </summary>
        public GraphFields? Fields { get; set; } = null;

        /// <summary>
        /// Optional interface of the interface.
        /// </summary>
        public GraphInterfaces? Interface { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => "[GraphInterfaceDefinition]";
    }
}
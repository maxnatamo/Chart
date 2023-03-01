namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of an interface in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#InterfaceTypeDefinition">Original documentation</seealso>
    public class GraphInterfaceDefinition : GraphDefinition
    {
        /// <summary>
        /// Set the kind of definition.
        /// </summary>
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Interface;

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

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphInterfaceDefinition]";
        }
    }
}
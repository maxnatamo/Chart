namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a definition in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Definition">Original documentation</seealso>
    public abstract class GraphExtensionDefinition : GraphDefinition
    {
        /// <summary>
        /// Set the kind of definition.
        /// </summary>
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Extension;

        /// <summary>
        /// The type of extension in the document.
        /// </summary>
        public abstract GraphExtensionKind ExtensionKind { get; }

        /// <summary>
        /// Optional directives for the extension.
        /// </summary>
        public GraphDirectives? Directives { get; set; }

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public abstract override string ToString();
    }
}
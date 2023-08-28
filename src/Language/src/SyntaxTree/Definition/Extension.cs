namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a definition in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Definition">Original documentation</seealso>
    public abstract class GraphExtensionDefinition : GraphDefinition, IHasDirectives
    {
        /// <inheritdoc />
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Extension;

        /// <inheritdoc />
        public override bool Executable => false;

        /// <summary>
        /// The type of extension in the document.
        /// </summary>
        public abstract GraphExtensionKind ExtensionKind { get; }

        /// <summary>
        /// Optional directives for the extension.
        /// </summary>
        public GraphDirectives? Directives { get; set; }

        /// <inheritdoc />
        public abstract override string ToString();
    }
}
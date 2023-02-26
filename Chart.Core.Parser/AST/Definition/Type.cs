namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a type in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#sec-Language.Operations">Original documentation</seealso>
    public abstract class GraphTypeDefinition : GraphDefinition
    {
        /// <summary>
        /// Set the kind of operation.
        /// </summary>
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Type;

        /// <summary>
        /// The kind of type this definition represents.
        /// </summary>
        public abstract GraphTypeDefinitionKind TypeKind { get; }

        /// <summary>
        /// Optional directives for the type.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public abstract override string ToString();
    }
}
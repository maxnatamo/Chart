namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a type in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#sec-Language.Operations">Original documentation</seealso>
    public abstract class GraphTypeDefinition : GraphDefinition, IHasDirectives
    {
        /// <inheritdoc />
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Type;

        /// <inheritdoc />
        public override bool Executable => false;

        /// <summary>
        /// The kind of type this definition represents.
        /// </summary>
        public abstract GraphTypeDefinitionKind TypeKind { get; }

        /// <summary>
        /// Optional directives for the type.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <inheritdoc />
        public abstract override string ToString();
    }
}
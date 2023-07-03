namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of an input-object in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#InputObjectTypeDefinition">Original documentation</seealso>
    public class GraphInputDefinition : GraphDefinition
    {
        /// <inheritdoc />
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Input;

        /// <inheritdoc />
        public override bool Executable => false;

        /// <summary>
        /// Optional directives for the type.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <summary>
        /// Underlying arguments of the type definition.
        /// </summary>
        public GraphArgumentsDefinition Arguments { get; set; } = default!;

        /// <inheritdoc />
        public override string ToString() => "[GraphInputDefinition]";
    }
}
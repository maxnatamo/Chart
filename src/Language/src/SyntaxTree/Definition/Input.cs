namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of an input-object in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#InputObjectTypeDefinition">Original documentation</seealso>
    public class GraphInputDefinition : GraphDefinition, IHasDirectives
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
        /// Underlying fields of the type definition.
        /// </summary>
        public GraphInputFieldsDefinition? Fields { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => "[GraphInputDefinition]";
    }
}
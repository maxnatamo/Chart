namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a definition in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Definition">Original documentation</seealso>
    public class GraphEnumDefinition : GraphDefinition, IHasDirectives
    {
        /// <inheritdoc />
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Enum;

        /// <inheritdoc />
        public override bool Executable => false;

        /// <summary>
        /// Optional directives of the enum.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <summary>
        /// List of values in the enum.
        /// </summary>
        public GraphEnumDefinitionValues? Values { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => "[GraphEnumDefinition]";
    }
}
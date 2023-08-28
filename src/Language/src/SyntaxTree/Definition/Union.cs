namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a definition in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Definition">Original documentation</seealso>
    public class GraphUnionDefinition : GraphDefinition, IHasDirectives
    {
        /// <inheritdoc />
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Union;

        /// <inheritdoc />
        public override bool Executable => false;

        /// <summary>
        /// Optional directives for the operation.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <summary>
        /// List of members in the union.
        /// </summary>
        public GraphUnionMembers? Members { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => $"[GraphUnionDefinition]";
    }
}
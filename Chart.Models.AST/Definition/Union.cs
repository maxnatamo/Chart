namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a definition in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Definition">Original documentation</seealso>
    public class GraphUnionDefinition : GraphDefinition
    {
        /// <summary>
        /// Set the kind of definition.
        /// </summary>
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Union;

        /// <summary>
        /// Whether the definition is executable, as per the GraphQL-spec.
        /// </summary>
        public override bool Executable => false;

        /// <summary>
        /// Optional directives for the operation.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <summary>
        /// List of members in the union.
        /// </summary>
        public GraphUnionMembers? Members { get; set; } = null;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphUnion]";
        }
    }
}
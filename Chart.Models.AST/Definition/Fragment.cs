namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a definition in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Definition">Original documentation</seealso>
    public class GraphFragmentDefinition : GraphDefinition
    {
        /// <summary>
        /// Set the kind of definition.
        /// </summary>
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Fragment;

        /// <summary>
        /// Whether the definition is executable, as per the GraphQL-spec.
        /// </summary>
        public override bool Executable => true;

        /// <summary>
        /// The type to apply the fragment to.
        /// </summary>
        public GraphNamedType Type { get; set; } = default!;

        /// <summary>
        /// Optional directives for the extension.
        /// </summary>
        public GraphDirectives? Directives { get; set; }

        /// <summary>
        /// List of nodes in the enum.
        /// </summary>
        public GraphSelectionSet SelectionSet { get; set; } = new GraphSelectionSet();

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphFragmentDefinition]";
        }
    }
}
namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a definition in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Definition">Original documentation</seealso>
    public class GraphEnumDefinition : GraphDefinition
    {
        /// <summary>
        /// Set the kind of definition.
        /// </summary>
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Enum;

        /// <summary>
        /// Whether the definition is executable, as per the GraphQL-spec.
        /// </summary>
        public override bool Executable => false;

        /// <summary>
        /// Optional directives of the enum.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <summary>
        /// List of values in the enum.
        /// </summary>
        public GraphEnumDefinitionValues Values { get; set; } = default!;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphEnum] {Name?.Value}";
        }
    }
}
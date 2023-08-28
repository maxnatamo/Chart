namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Directive definition in the schema.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#DirectiveDefinition">Original documentation</seealso>
    public class GraphDirectiveDefinition : GraphDefinition
    {
        /// <inheritdoc />
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Directive;

        /// <inheritdoc />
        public override bool Executable => false;

        /// <summary>
        /// Optional arguments for the directive.
        /// </summary>
        public GraphArgumentsDefinition? Arguments { get; set; } = null;

        /// <summary>
        /// Whether the directive is repeatable.
        /// </summary>
        public bool Repeatable { get; set; } = false;

        /// <summary>
        /// List of directive locations selected by the directive.
        /// </summary>
        public GraphDirectiveLocations Locations { get; set; } = new GraphDirectiveLocations();

        /// <inheritdoc />
        public override string ToString() => "[GraphDirectiveDefinition]";
    }
}
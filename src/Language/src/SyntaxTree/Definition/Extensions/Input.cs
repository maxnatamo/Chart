namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of an input extension in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#EnumTypeExtension">Original documentation</seealso>
    public class GraphInputExtension : GraphExtensionDefinition
    {
        /// <inheritdoc />
        public override GraphExtensionKind ExtensionKind => GraphExtensionKind.Input;

        /// <summary>
        /// Underlying arguments of the type definition.
        /// </summary>
        public GraphArgumentsDefinition? Arguments { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => "[GraphInputExtension]";
    }
}
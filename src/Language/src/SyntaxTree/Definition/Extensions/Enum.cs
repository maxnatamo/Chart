namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of an enum extension in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#EnumTypeExtension">Original documentation</seealso>
    public class GraphEnumExtension : GraphExtensionDefinition
    {
        /// <inheritdoc />
        public override GraphExtensionKind ExtensionKind => GraphExtensionKind.Enum;

        /// <summary>
        /// List of values in the enum.
        /// </summary>
        public GraphEnumDefinitionValues? Values { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => "[GraphEnumExtension]";
    }
}
namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a union extension in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#UnionTypeExtension">Original documentation</seealso>
    public class GraphUnionExtension : GraphExtensionDefinition
    {
        /// <inheritdoc />
        public override GraphExtensionKind ExtensionKind => GraphExtensionKind.Union;

        /// <summary>
        /// List of members in the union.
        /// </summary>
        public GraphUnionMembers? Members { get; set; } = null;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString() => "[GraphUnionExtension]";
    }
}
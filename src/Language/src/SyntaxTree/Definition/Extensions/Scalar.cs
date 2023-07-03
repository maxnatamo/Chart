namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a scalar extension in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ScalarTypeExtension">Original documentation</seealso>
    public class GraphScalarExtension : GraphExtensionDefinition
    {
        /// <inheritdoc />
        public override GraphExtensionKind ExtensionKind => GraphExtensionKind.Scalar;

        /// <inheritdoc />
        public override string ToString() => "[GraphScalarExtension]";
    }
}
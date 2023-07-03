namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a scalar-type in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ScalarTypeDefinition">Original documentation</seealso>
    public class GraphScalarType : GraphTypeDefinition
    {
        /// <inheritdoc />
        public override GraphTypeDefinitionKind TypeKind => GraphTypeDefinitionKind.Scalar;

        /// <inheritdoc />
        public override string ToString() => $"[GraphType] Scalar";
    }
}
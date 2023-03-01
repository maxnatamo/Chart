namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a scalar-type in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ScalarTypeDefinition">Original documentation</seealso>
    public class GraphScalarType : GraphTypeDefinition
    {
        /// <summary>
        /// Set the type of the operation to Scalar.
        /// </summary>
        public override GraphTypeDefinitionKind TypeKind => GraphTypeDefinitionKind.Scalar;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphType] Scalar";
        }
    }
}
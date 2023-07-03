namespace Chart.Language.SyntaxTree
{
    public enum GraphTypeDefinitionKind
    {
        /// <summary>
        /// Scalar-type definition.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#ScalarTypeDefinition">Original documentation</seealso>
        Scalar,

        /// <summary>
        /// Object-type definition.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#ObjectTypeDefinition">Original documentation</seealso>
        Object,
    }
}
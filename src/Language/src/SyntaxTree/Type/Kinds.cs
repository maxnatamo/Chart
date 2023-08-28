namespace Chart.Language.SyntaxTree
{
    public enum GraphTypeKind
    {
        /// <summary>
        /// List of objects inside of a type-definition.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#ListType">Original documentation</seealso>
        List,

        /// <summary>
        /// Named type inside of a type-definition.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#NamedType">Original documentation</seealso>
        Named,
    }
}
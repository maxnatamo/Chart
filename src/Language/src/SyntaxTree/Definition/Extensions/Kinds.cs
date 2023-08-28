namespace Chart.Language.SyntaxTree
{
    public enum GraphExtensionKind
    {
        /// <summary>
        /// Definition of a scalar extension in the root GraphQL-document.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#ScalarTypeExtension">Original documentation</seealso>
        Scalar,

        /// <summary>
        /// Definition of an object extension in the root GraphQL-document.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#ObjectTypeExtension">Original documentation</seealso>
        Object,

        /// <summary>
        /// Definition of a scema extension in the root GraphQL-document.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#SchemaExtension">Original documentation</seealso>
        Schema,

        /// <summary>
        /// Definition of an interface extension in the root GraphQL-document.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#InterfaceTypeExtension">Original documentation</seealso>
        Interface,

        /// <summary>
        /// Definition of an union extension in the root GraphQL-document.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#UnionTypeExtension">Original documentation</seealso>
        Union,

        /// <summary>
        /// Definition of an enum extension in the root GraphQL-document.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#EnumTypeExtension">Original documentation</seealso>
        Enum,

        /// <summary>
        /// Definition of an input extension in the root GraphQL-document.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#InputObjectTypeExtension">Original documentation</seealso>
        Input,
    }
}
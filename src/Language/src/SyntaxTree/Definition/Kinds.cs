namespace Chart.Language.SyntaxTree
{
    public enum GraphDefinitionKind
    {
        /// <summary>
        /// Operation definition for query, mutation and subscription operations.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#OperationDefinition">Original documentation</seealso>
        Operation,

        /// <summary>
        /// Type definition for the schema.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#TypeDefinition">Original documentation</seealso>
        Type,

        /// <summary>
        /// Definition of an input-object in the root GraphQL-document.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#InputObjectTypeDefinition">Original documentation</seealso>
        Input,

        /// <summary>
        /// Enum definition in the schema.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#EnumTypeDefinition">Original documentation</seealso>
        Enum,

        /// <summary>
        /// Enum definition in the schema.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#UnionTypeDefinition">Original documentation</seealso>
        Union,

        /// <summary>
        /// Fragment definition in the schema.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#FragmentDefinition">Original documentation</seealso>
        Fragment,

        /// <summary>
        /// Directive definition in the schema.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#DirectiveDefinition">Original documentation</seealso>
        Directive,

        /// <summary>
        /// Type extension definition in the schema.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#TypeSystemExtension">Original documentation</seealso>
        Extension,

        /// <summary>
        /// Definition of a schema in the root GraphQL-document.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#SchemaDefinition">Original documentation</seealso>
        Schema,

        /// <summary>
        /// Definition of an interface in the root GraphQL-document.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#InterfaceTypeDefinition">Original documentation</seealso>
        Interface,
    }
}
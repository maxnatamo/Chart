namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a directive location.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#DirectiveLocation">Original documentation</seealso>
    [Flags]
    public enum GraphDirectiveLocationFlags
    {
        QUERY = (1 << 0),
        MUTATION = (1 << 1),
        SUBSCRIPTION = (1 << 2),
        FIELD = (1 << 3),
        FRAGMENT_DEFINITION = (1 << 4),
        FRAGMENT_SPREAD = (1 << 5),
        INLINE_FRAGMENT = (1 << 6),
        VARIABLE_DEFINITION = (1 << 7),

        SCHEMA = (1 << 8),
        SCALAR = (1 << 9),
        OBJECT = (1 << 10),
        FIELD_DEFINITION = (1 << 11),
        ARGUMENT_DEFINITION = (1 << 12),
        INTERFACE = (1 << 13),
        UNION = (1 << 14),
        ENUM = (1 << 15),
        ENUM_VALUE = (1 << 16),
        INPUT_OBJECT = (1 << 17),
        INPUT_FIELD_DEFINITION = (1 << 18),
    }
}
namespace Chart.Core
{
    /// <summary>
    /// Available locations on a directive definition.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#DirectiveLocation">Corresponding specification.</seealso>
    public enum DirectoryLocation
    {
        Query                       = (1 << 0),
        Mutation                    = (1 << 1),
        Subscription                = (1 << 2),
        Field                       = (1 << 3),
        FragmentDefinition          = (1 << 4),
        FragmentSpread              = (1 << 5),
        InlineFragment              = (1 << 6),
        VariableDefinition          = (1 << 7),
        Schema                      = (1 << 8),
        Scalar                      = (1 << 9),
        Object                      = (1 << 10),
        FieldDefinition             = (1 << 11),
        ArgumentDefinition          = (1 << 12),
        Interface                   = (1 << 13),
        Union                       = (1 << 14),
        Enum                        = (1 << 15),
        EnumValue                   = (1 << 16),
        InputObject                 = (1 << 17),
        InputFieldDefinition        = (1 << 18),
    }
}
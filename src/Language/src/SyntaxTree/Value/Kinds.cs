namespace Chart.Language.SyntaxTree
{
    public enum GraphValueKind
    {
        /// <summary>
        /// Value referencing a variable.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#Variable">Original documentation</seealso>
        Variable,

        /// <summary>
        /// Integer value
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#IntValue">Original documentation</seealso>
        Int,

        /// <summary>
        /// Floating-point value.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#FloatValue">Original documentation</seealso>
        Float,

        /// <summary>
        /// String value
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#StringValue">Original documentation</seealso>
        String,

        /// <summary>
        /// Boolean value
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#BooleanValue">Original documentation</seealso>
        Boolean,

        /// <summary>
        /// Null/undefined value
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#NullValue">Original documentation</seealso>
        Null,

        /// <summary>
        /// Enum value
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#EnumValue">Original documentation</seealso>
        Enum,

        /// <summary>
        /// List value
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#ListValue">Original documentation</seealso>
        List,

        /// <summary>
        /// Object value
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#ObjectValue">Original documentation</seealso>
        Object,
    }
}
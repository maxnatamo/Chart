using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    /// <summary>
    /// Base scalar type for the types in GraphQL.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#sec-Types">Original documentation.</seealso>
    public abstract class GraphBaseType
    {
        /// <summary>
        /// Name of the type in GraphQL.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Parse a GraphValue into the matching C#-type.
        /// </summary>
        /// <example>
        /// Passing a GraphStringValue will return the content as a C# string.
        /// </example>
        /// <remarks>
        /// Passing a GraphNullValue-object will return null.
        /// </remarks>
        /// <param name="value">The GraphValue to parse.</param>
        /// <returns>C#-native value of the value content.</returns>
        public abstract object? ParseValue(GraphValue value);

        /// <summary>
        /// Parse a C# native type into a GraphValue-object.
        /// </summary>
        /// <example>
        /// Passing a string value will return a GraphStringValue with the stirng content..
        /// </example>
        /// <remarks>
        /// Passing a null value will return GraphNullValue.
        /// </remarks>
        /// <param name="value">The value to parse.</param>
        /// <returns>Corresponding GraphValue-type.</returns>
        public abstract GraphValue ParseLiteral(object? value);

        /// <summary>
        /// Serialize a C# native-type into the GraphQL schema format.
        /// </summary>
        /// <example>
        /// Passing a string, of value 'test', would return "test" in double-quotes.
        /// </example>
        /// <remarks>
        /// If the input value is null, the return string is "null" in double-quotes.
        /// </remarks>
        /// <param name="value">The type to serialize to GraphQL.</param>
        /// <returns>The serialized value.</returns>
        public abstract string? Serialize(object? value);
    }
}
using Chart.Models.AST;

namespace Chart.Core.TypeResolver
{
    /// <summary>
    /// Scalar type for the String-type in GraphQL.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#sec-String">Original documentation.</seealso>
    public class StringScalarType : GraphBaseType
    {
        /// <inheritdoc cref="GraphBaseType.Name" />
        public override string Name => "String";

        /// <inheritdoc cref="GraphBaseType.ParseValue(GraphValue)" />
        public override object? ParseValue(GraphValue value)
        {
            if(value is GraphNullValue)
            {
                return null;
            }

            if(value is GraphStringValue stringValue)
            {
                return stringValue.Value;
            }

            throw new ArgumentException("Failed to parse literal.");
        }

        /// <inheritdoc cref="GraphBaseType.ParseLiteral(object?)" />
        public override GraphValue ParseLiteral(object? value)
        {
            if(value == null)
            {
                return new GraphNullValue();
            }

            if(value is string stringInput)
            {
                return new GraphStringValue(stringInput);
            }

            throw new ArgumentException("Failed to parse value.");
        }

        /// <inheritdoc cref="GraphBaseType.Serialize(object?)" />
        public override string? Serialize(object? value)
        {
            if(value == null)
            {
                return "null";
            }

            if(value is string str)
            {
                return $"\"{str}\"";
            }

            throw new ArgumentException("Failed to serialize literal.");
        }
    }
}
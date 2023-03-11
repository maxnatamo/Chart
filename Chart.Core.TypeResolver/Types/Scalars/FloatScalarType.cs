using Chart.Models.AST;

namespace Chart.Core.TypeResolver
{
    /// <summary>
    /// Scalar type for the Float-type in GraphQL.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#sec-Float">Original documentation.</seealso>
    public class FloatScalarType : GraphBaseType
    {
        /// <inheritdoc cref="GraphBaseType.Name" />
        public override string Name => "Float";

        /// <inheritdoc cref="GraphBaseType.ParseValue(GraphValue)" />
        public override object? ParseValue(GraphValue value)
        {
            if(value is GraphNullValue)
            {
                return null;
            }

            if(value is GraphFloatValue floatValue)
            {
                return floatValue.Value;
            }

            throw new ArgumentException("Failed to parse literal.");
        }

        /// <inheritdoc cref="GraphBaseType.ParseLiteral(object?)" />
        public override GraphValue ParseLiteral(object? value)
        {
            return value switch
            {
                null => new GraphNullValue(),
                Single => new GraphFloatValue(Convert.ToSingle(value)),
                Double => new GraphFloatValue(Convert.ToSingle(value)),
                Decimal => new GraphFloatValue(Convert.ToSingle(value)),

                _ => throw new ArgumentException("Failed to parse value.")
            };
        }

        /// <inheritdoc cref="GraphBaseType.Serialize(object?)" />
        public override string? Serialize(object? value)
        {
            return value switch
            {
                null => "null",
                Single => value.ToString(),
                Double => value.ToString(),
                Decimal => value.ToString(),

                _ => throw new ArgumentException("Failed to parse value.")
            };

            throw new ArgumentException("Failed to serialize literal.");
        }
    }
}
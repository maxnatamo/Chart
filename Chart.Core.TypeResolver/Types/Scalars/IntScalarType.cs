using Chart.Models.AST;

namespace Chart.Core.TypeResolver
{
    /// <summary>
    /// Scalar type for the Int-type in GraphQL.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#sec-Int">Original documentation.</seealso>
    public class IntScalarType : GraphBaseType
    {
        /// <inheritdoc cref="GraphBaseType.Name" />
        public override string Name => "Int";

        /// <inheritdoc cref="GraphBaseType.ParseValue(GraphValue)" />
        public override object? ParseValue(GraphValue value)
        {
            if(value is GraphNullValue)
            {
                return null;
            }

            if(value is GraphIntValue intValue)
            {
                return intValue.Value;
            }

            throw new ArgumentException("Failed to parse literal.");
        }

        /// <inheritdoc cref="GraphBaseType.ParseLiteral(object?)" />
        public override GraphValue ParseLiteral(object? value)
        {
            return value switch
            {
                null => new GraphNullValue(),
                SByte => new GraphIntValue(Convert.ToInt32(value)),
                Int16 => new GraphIntValue(Convert.ToInt32(value)),
                Int32 => new GraphIntValue(Convert.ToInt32(value)),
                Int64 => new GraphIntValue(Convert.ToInt32(value)),
                Int128 => new GraphIntValue(Convert.ToInt32(value)),
                Byte => new GraphIntValue(Convert.ToInt32(value)),
                UInt16 => new GraphIntValue(Convert.ToInt32(value)),
                UInt32 => new GraphIntValue(Convert.ToInt32(value)),
                UInt64 => new GraphIntValue(Convert.ToInt32(value)),
                UInt128 => new GraphIntValue(Convert.ToInt32(value)),

                _ => throw new ArgumentException("Failed to parse value.")
            };
        }

        /// <inheritdoc cref="GraphBaseScalarType.Serialize(object?)" />
        public override string? Serialize(object? value)
        {
            return value switch
            {
                null => "null",
                SByte => value.ToString(),
                Int16 => value.ToString(),
                Int32 => value.ToString(),
                Int64 => value.ToString(),
                Int128 => value.ToString(),
                Byte => value.ToString(),
                UInt16 => value.ToString(),
                UInt32 => value.ToString(),
                UInt64 => value.ToString(),
                UInt128 => value.ToString(),

                _ => throw new ArgumentException("Failed to parse value.")
            };

            throw new ArgumentException("Failed to serialize literal.");
        }
    }
}
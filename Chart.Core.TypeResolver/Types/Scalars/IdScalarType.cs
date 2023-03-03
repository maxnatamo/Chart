using Chart.Models.AST;

namespace Chart.Core.TypeResolver
{
    /// <summary>
    /// Scalar type for the ID-type in GraphQL.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#sec-ID">Original documentation.</seealso>
    public class IdScalarType : GraphBaseType
    {
        /// <inheritdoc cref="GraphBaseType.Name" />
        public override string Name => "ID";

        /// <inheritdoc cref="GraphBaseScalarType.ParseValue(GraphValue)" />
        public override object? ParseValue(GraphValue value)
        {
            return value switch
            {
                GraphNullValue      => null,
                GraphStringValue    => ((GraphStringValue) value).Value,
                GraphIntValue       => ((GraphIntValue) value).Value,

                _                   => throw new ArgumentException("Failed to parse value.")
            };
        }

        /// <inheritdoc cref="GraphBaseType.ParseLiteral(object?)" />
        public override GraphValue ParseLiteral(object? value)
        {
            return value switch
            {
                null        => new GraphNullValue(),
                String      => new GraphStringValue((String) value),
                SByte       => new GraphIntValue(Convert.ToInt32(value)),
                Int16       => new GraphIntValue(Convert.ToInt32(value)),
                Int32       => new GraphIntValue(Convert.ToInt32(value)),
                Int64       => new GraphIntValue(Convert.ToInt32(value)),
                Int128      => new GraphIntValue(Convert.ToInt32(value)),
                Byte        => new GraphIntValue(Convert.ToInt32(value)),
                UInt16      => new GraphIntValue(Convert.ToInt32(value)),
                UInt32      => new GraphIntValue(Convert.ToInt32(value)),
                UInt64      => new GraphIntValue(Convert.ToInt32(value)),
                UInt128     => new GraphIntValue(Convert.ToInt32(value)),

                _           => throw new ArgumentException("Failed to parse literal.")
            };
        }

        /// <inheritdoc cref="GraphBaseType.Serialize(object?)" />
        public override string? Serialize(object? value)
        {
            return value switch
            {
                null        => "null",
                String      => $"\"{value.ToString()}\"",
                SByte       => value.ToString(),
                Int16       => value.ToString(),
                Int32       => value.ToString(),
                Int64       => value.ToString(),
                Int128      => value.ToString(),
                Byte        => value.ToString(),
                UInt16      => value.ToString(),
                UInt32      => value.ToString(),
                UInt64      => value.ToString(),
                UInt128     => value.ToString(),

                _           => throw new ArgumentException("Failed to parse value.")
            };

            throw new ArgumentException("Failed to serialize literal.");
        }
    }
}
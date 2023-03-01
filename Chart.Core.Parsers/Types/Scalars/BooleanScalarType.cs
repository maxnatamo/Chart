using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    /// <summary>
    /// Scalar type for the Boolean-type in GraphQL.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#sec-Boolean">Original documentation.</seealso>
    public class BooleanScalarType : GraphBaseType
    {
        /// <inheritdoc cref="GraphBaseType.Name" />
        public override string Name => "Boolean";

        /// <inheritdoc cref="GraphBaseType.ParseValue(GraphValue)" />
        public override object? ParseValue(GraphValue value)
        {
            if(value is GraphNullValue)
            {
                return null;
            }

            if(value is GraphBooleanValue floatValue)
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
                null        => new GraphNullValue(),
                Boolean     => new GraphBooleanValue(Convert.ToBoolean(value)),

                _           => throw new ArgumentException("Failed to parse value.")
            };
        }

        /// <inheritdoc cref="GraphBaseType.Serialize(object?)" />
        public override string? Serialize(object? value)
        {
            return value switch
            {
                null        => "null",
                Boolean     => value.ToString()?.ToLower(),

                _           => throw new ArgumentException("Failed to parse value.")
            };

            throw new ArgumentException("Failed to serialize literal.");
        }
    }
}
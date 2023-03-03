using Chart.Models.AST;

namespace Chart.Core.TypeResolver
{
    /// <summary>
    /// Scalar type for the Boolean-type in GraphQL.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#sec-Boolean">Original documentation.</seealso>
    public class ObjectType : GraphBaseType
    {
        /// <inheritdoc cref="GraphBaseType.Name" />
        public override string Name => "Object";

        /// <inheritdoc cref="GraphBaseType.ParseValue(GraphValue)" />
        public override object? ParseValue(GraphValue value)
        {
            if(value is GraphNullValue)
            {
                return null;
            }

            if(value is GraphObjectValue objValue)
            {
                return objValue.Fields;
            }

            throw new ArgumentException("Failed to parse value.");
        }

        /// <inheritdoc cref="GraphBaseType.ParseLiteral(object?)" />
        public override GraphValue ParseLiteral(object? value)
        {
            if(value == null)
            {
                return new GraphNullValue();
            }

            if(!value.GetType().IsPrimitive)
            {
                Console.WriteLine($"{value.ToString()} => {value.GetType().GetMembers().Count()}");
                return new GraphObjectValue();
            }

            throw new ArgumentException("Failed to parse literal.");
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

            throw new ArgumentException("Failed to serialize.");
        }
    }
}
using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public interface ITypeChecker
    {
        /// <summary>
        /// Returns whether the given type can contain the given value.
        /// </summary>
        bool CanContainValue(GraphType type, IGraphValue value, QueryExecutionContext context);
    }

    /// <summary>
    /// Utility class for checking compatibility between types and values.
    /// </summary>
    public class TypeChecker : ITypeChecker
    {
        private readonly IValueCoercer _valueCoercer;

        public TypeChecker(IValueCoercer valueCoercer)
            => this._valueCoercer = valueCoercer;

        /// <inheritdoc />
        public bool CanContainValue(
            GraphType type,
            IGraphValue value,
            QueryExecutionContext context) =>
            (type, value) switch
            {
                // Non-nullable type cannot contain null value
                (_, GraphNullValue) when type.NonNullable => false,

                // Nullable type can contain null value
                (_, GraphNullValue) when !type.NonNullable => true,

                // Validate all list-items, recursively
                (GraphListType _type, GraphListValue _value) => _value.Value.All(v => this.CanContainValue(_type.UnderlyingType, v, context)),

                // List type cannot contain non-list values
                (GraphListType, _) when value.ValueKind != GraphValueKind.List => false,

                // List values cannot be contained within non-list types
                (_, GraphListValue) when type.TypeKind != GraphTypeKind.List => false,

                // Handle any named types
                (GraphNamedType _type, _) => value switch
                {
                    // Scalar types
                    _ when ScalarTypes.IsDefaultScalar(_type.Name) => this._valueCoercer.CoerceInput(value, type) != null,

                    // Enum types
                    GraphEnumValue _value => TypeChecker.CanContainEnumValue(_type, _value, context),

                    // Object types
                    GraphObjectValue _value => this.CanContainObjectValue(_type, _value, context),

                    _ => false
                },

                _ => throw new NotSupportedException()
            };

        /// <summary>
        /// Returns whether the given enum type can contain the given value.
        /// </summary>
        protected internal static bool CanContainEnumValue(
            GraphNamedType type,
            GraphEnumValue value,
            QueryExecutionContext context)
        {
            if(!context.Schema.TryGetDefinition(type.Name, out GraphEnumDefinition? enumDefinition))
            {
                return false;
            }

            if(enumDefinition.Values is null)
            {
                return false;
            }

            return enumDefinition.Values.Values.Count(v => v.Name == value.Value) == 1;
        }

        /// <summary>
        /// Returns whether the given object type can contain the given value.
        /// </summary>
        protected internal bool CanContainObjectValue(
            GraphNamedType type,
            GraphObjectValue value,
            QueryExecutionContext context)
        {
            if(!context.Schema.TryGetDefinition(type.Name, out GraphObjectType? objectDefinition))
            {
                return false;
            }

            if(objectDefinition.Fields is null)
            {
                return false;
            }

            foreach(GraphField objectField in objectDefinition.Fields.Fields)
            {
                if(!value.Value.TryGetValue(objectField.Name, out IGraphValue? valueField))
                {
                    return false;
                }

                if(!this.CanContainValue(objectField.Type, valueField, context))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
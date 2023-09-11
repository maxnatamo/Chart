using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public interface IValueCoercer
    {
        /// <summary>
        /// Try to coerce the given input value to the given graph type.
        /// </summary>
        /// <param name="value">The input value to coerce.</param>
        /// <param name="type">The type to attempt coercing to.</param>
        /// <returns>The coerced value, if successful. Otherwise, <see langword="null" />.</returns>
        IGraphValue? CoerceInput(IGraphValue value, GraphType type);

        /// <inheritdoc cref="IValueCoercer.CoerceInput(IGraphValue, GraphType)" />
        IGraphValue? CoerceInput(IGraphValue value, GraphListType type);

        /// <inheritdoc cref="IValueCoercer.CoerceInput(IGraphValue, GraphType)" />
        IGraphValue? CoerceInput(IGraphValue value, GraphNamedType type);

        /// <summary>
        /// Try to coerce the given runtime value to the given graph type.
        /// </summary>
        /// <param name="value">The value to coerce.</param>
        /// <param name="type">The type to attempt coercing to.</param>
        /// <returns>The coerced value, if successful. Otherwise, <see langword="null" />.</returns>
        object? CoerceResult(object? value, GraphType type);

        /// <inheritdoc cref="IValueCoercer.CoerceResult(object?, GraphType)" />
        object? CoerceResult(object? value, GraphListType type);

        /// <inheritdoc cref="IValueCoercer.CoerceResult(object?, GraphType)" />
        object? CoerceResult(object? value, GraphNamedType type);
    }

    public class ValueCoercer : IValueCoercer
    {
        private readonly ITypeRegistry _typeRegistry;
        private readonly ITypeResolver _typeResolver;

        public ValueCoercer(ITypeRegistry typeRegistry, ITypeResolver typeResolver)
        {
            this._typeRegistry = typeRegistry;
            this._typeResolver = typeResolver;
        }

        /// <inheritdoc />
        public IGraphValue? CoerceInput(IGraphValue value, GraphType type) =>
            type switch
            {
                GraphListType _type => this.CoerceInput(value, _type),
                GraphNamedType _type => this.CoerceInput(value, _type),

                _ => throw new NotSupportedException()
            };

        /// <inheritdoc />
        public IGraphValue? CoerceInput(IGraphValue value, GraphListType type)
        {
            if(value is not GraphListValue listValue)
            {
                return null;
            }

            List<IGraphValue> values = new();

            foreach(IGraphValue _value in listValue.Value)
            {
                IGraphValue? coercedValue = this.CoerceInput(_value, type.UnderlyingType);
                if(coercedValue is null && type.UnderlyingType.NonNullable)
                {
                    return null;
                }

                coercedValue ??= new GraphNullValue();
                values.Add(coercedValue);
            }

            return new GraphListValue(values);
        }

        /// <inheritdoc />
        public IGraphValue? CoerceInput(IGraphValue value, GraphNamedType type)
        {
            if(!this._typeRegistry.TryGetType(type.Name, out RegisteredType? registeredType))
            {
                throw new NotImplementedException();
            }

            return this.CoerceInput(value, registeredType.Value.TypeDefinition);
        }

        private IGraphValue? CoerceInput(IGraphValue value, TypeDefinition typeDefinition) =>
            (value, typeDefinition) switch
            {
                (_, ScalarType _type) when value.ValueKind.IsScalar() => this.CoerceInput(value, _type),
                (GraphObjectValue _value, InputObjectType _type) => this.CoerceInput(_value, _type),

                _ => null
            };

        private IGraphValue? CoerceInput(IGraphValue value, ScalarType scalarType)
        {
            if(!scalarType.IsOfType(value))
            {
                return null;
            }

            return scalarType.Resolver.CoerceInput(value);
        }

        private IGraphValue? CoerceInput(GraphObjectValue value, InputObjectType inputObjectType)
        {
            GraphObjectValue coercedValue = new();

            foreach(KeyValuePair<GraphName, IGraphValue> field in value.Value)
            {
                InputFieldDefinition? fieldDefinition = inputObjectType.Fields.FirstOrDefault(f => f.Name == field.Key)
                    ?? throw new NotImplementedException();

                GraphType fieldType = this._typeResolver.ResolveDefinition(fieldDefinition.Type);
                IGraphValue? coercedField = this.CoerceInput(field.Value, fieldType);

                if(coercedField is null)
                {
                    return null;
                }

                coercedValue.Value.Add(field.Key, coercedField);
            }

            return coercedValue;
        }

        /// <inheritdoc />
        public object? CoerceResult(object? value, GraphType type) =>
            type switch
            {
                GraphListType _type => this.CoerceResult(value, _type),
                GraphNamedType _type => this.CoerceResult(value, _type),

                _ => throw new NotSupportedException()
            };

        /// <inheritdoc />
        public object? CoerceResult(object? value, GraphListType type)
        {
            if(value is not GraphListValue listValue)
            {
                return null;
            }

            List<object?> values = new();

            foreach(IGraphValue _value in listValue.Value)
            {
                object? coercedValue = this.CoerceResult(_value, type.UnderlyingType);
                values.Add(coercedValue);
            }

            return values;
        }

        /// <inheritdoc />
        public object? CoerceResult(object? value, GraphNamedType type)
        {
            if(!this._typeRegistry.TryGetType(type.Name, out RegisteredType? registeredType))
            {
                throw new NotImplementedException();
            }

            return this.CoerceResult(value, registeredType.Value.TypeDefinition);
        }

        private object? CoerceResult(object? value, TypeDefinition typeDefinition) =>
            (value, typeDefinition) switch
            {
                (_, ObjectType) => value,

                _ => null
            };
    }
}
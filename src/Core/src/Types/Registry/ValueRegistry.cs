using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public interface IValueRegistry
    {
        /// <summary>
        /// Register a value binding; binding a .NET type to a GraphQL schema value.
        /// </summary>
        /// <param name="runtimeType">The .NET native type to bind.</param>
        /// <param name="schemaValueType">The GraphQL value type to bind the runtime type to.</param>
        void RegisterValueBinding(Type runtimeType, Type schemaValueType);

        /// <inheritdoc cref="IValueRegistry.RegisterValueBinding(Type, Type)" />
        void RegisterValueBinding(KeyValuePair<Type, Type> valueBinding);

        /// <inheritdoc cref="IValueRegistry.RegisterValueBinding(Type, Type)" />
        void RegisterValueBinding<TSchemaType>(Type runtimeType) where TSchemaType : IGraphValue;

        /// <inheritdoc cref="IValueRegistry.RegisterValueBinding(Type, Type)" />
        void RegisterValueBinding<TRuntime, TSchemaType>() where TSchemaType : IGraphValue;

        /// <summary>
        /// Resolve the given value and it's type to a GraphQL schema value type.
        /// </summary>
        IGraphValue ResolveValue(object? value);

        /// <summary>
        /// Attempt to resolve the given value and it's type to a GraphQL schema value type.
        /// </summary>
        bool TryResolveValue(object? value, [NotNullWhen(true)] out IGraphValue? graphValue);
    }

    /// <summary>
    /// Registry for handling and converting .NET native values to GraphQL values.
    /// </summary>
    public class ValueRegistry : IValueRegistry
    {
        private static readonly ReadOnlyDictionary<Type, Type> BuiltinValueBindings = new(
            new Dictionary<Type, Type>
            {
                { typeof(String),       typeof(GraphStringValue) },
                { typeof(Boolean),      typeof(GraphBooleanValue) },

                { typeof(Int64),        typeof(GraphIntValue) },
                { typeof(UInt64),       typeof(GraphIntValue) },
                { typeof(Int32),        typeof(GraphIntValue) },
                { typeof(UInt32),       typeof(GraphIntValue) },
                { typeof(Int16),        typeof(GraphIntValue) },
                { typeof(UInt16),       typeof(GraphIntValue) },
                { typeof(SByte),        typeof(GraphIntValue) },
                { typeof(Byte),         typeof(GraphIntValue) },

                { typeof(Single),       typeof(GraphFloatValue) },
                { typeof(Double),       typeof(GraphFloatValue) },
                { typeof(Decimal),      typeof(GraphFloatValue) },
            });

        private readonly Dictionary<Type, Type> _valueConversionTable;

        public ValueRegistry()
        {
            this._valueConversionTable = new Dictionary<Type, Type>();

            foreach(KeyValuePair<Type, Type> valueBinding in ValueRegistry.BuiltinValueBindings)
            {
                this.RegisterValueBinding(valueBinding);
            }
        }

        /// <inheritdoc />
        public void RegisterValueBinding(Type runtimeType, Type schemaValueType)
        {
            if(!schemaValueType.IsAssignableTo(typeof(IGraphValue)))
            {
                throw new ArgumentException($"Schema value type '{schemaValueType.Name}' is not derived from IGraphValue");
            }

            if(!schemaValueType.GetConstructors().Any(v => v.GetParameters().Length == 1))
            {
                throw new ArgumentException($"Schema value type '{schemaValueType.Name}' does not have any single-parameter constructor");
            }

            this._valueConversionTable.Add(runtimeType, schemaValueType);
        }

        /// <inheritdoc />
        public void RegisterValueBinding(KeyValuePair<Type, Type> valueBinding)
            => this.RegisterValueBinding(valueBinding.Key, valueBinding.Value);

        /// <inheritdoc />
        public void RegisterValueBinding<TSchemaType>(Type runtimeType)
            where TSchemaType : IGraphValue
            => this.RegisterValueBinding(runtimeType, typeof(TSchemaType));

        /// <inheritdoc />
        public void RegisterValueBinding<TRuntime, TSchemaType>()
            where TSchemaType : IGraphValue
            => this.RegisterValueBinding(typeof(TRuntime), typeof(TSchemaType));

        /// <inheritdoc />
        public virtual IGraphValue ResolveValue(object? value)
        {
            if(!this.TryResolveValue(value, out IGraphValue? graphValue))
            {
                throw new KeyNotFoundException($"Runtime value type '{value?.GetType().Name ?? "null"}' was not found.");
            }

            return graphValue;
        }

        /// <inheritdoc />
        public virtual bool TryResolveValue(object? value, [NotNullWhen(true)] out IGraphValue? graphValue)
        {
            if(value is null)
            {
                graphValue = new GraphNullValue();
                return true;
            }

            return value.GetType() switch
            {
                Type t when t.IsOfGenericType(typeof(Dictionary<,>)) => this.TryGetDictionaryType(value, out graphValue),
                Type t when t.IsOfGenericType(typeof(IEnumerable)) => this.TryGetListType(value, out graphValue),
                Type t when t.IsAnonymous() => this.TryGetAnonymousType(value, out graphValue),

                _ => this.TryGetScalarType(value, out graphValue)
            };
        }

        /// <summary>
        /// Attempt to resolve the given value and it's type to a GraphQL schema scalar value.
        /// </summary>
        protected virtual bool TryGetScalarType(object value, [NotNullWhen(true)] out IGraphValue? graphValue)
        {
            Type valueType = value.GetType();

            if(!this._valueConversionTable.TryGetValue(valueType, out Type? graphValueType))
            {
                graphValue = null;
                return false;
            }

            try
            {
                graphValue = (IGraphValue?) Activator.CreateInstance(graphValueType, value);
                if(graphValue is null)
                {
                    throw new NullReferenceException($"Graph value type '{graphValueType.Name}' could not be referenced.");
                }
            }
            catch(Exception e)
            {
                throw new ArgumentException($"Graph value type '{graphValueType.Name}' could not be created.", e);
            }

            return true;
        }

        /// <summary>
        /// Attempt to resolve the given list object to a GraphQL list of values.
        /// </summary>
        protected virtual bool TryGetListType(object objectValue, [NotNullWhen(true)] out IGraphValue? graphValue)
        {
            IEnumerable listValue = (IEnumerable) objectValue;
            List<IGraphValue> values = new();

            foreach(object? element in listValue)
            {
                if(!this.TryResolveValue(element, out IGraphValue? elementGraphValue))
                {
                    graphValue = null;
                    return false;
                }

                values.Add(elementGraphValue);
            }

            graphValue = new GraphListValue(values);
            return true;
        }

        /// <summary>
        /// Attempt to resolve the given dictionary object to a GraphQL object.
        /// </summary>
        protected virtual bool TryGetDictionaryType(object objectValue, [NotNullWhen(true)] out IGraphValue? graphValue)
        {
            IDictionary dictionaryValue = (IDictionary) objectValue;
            Dictionary<GraphName, IGraphValue> values = new();

            foreach(DictionaryEntry element in dictionaryValue)
            {
                if(element.Key is not string)
                {
                    throw new NotImplementedException();
                }

                if(!this.TryResolveValue(element.Value, out IGraphValue? elementGraphValue))
                {
                    graphValue = null;
                    return false;
                }

                GraphName graphName = new((string) element.Key);
                values.Add(graphName, elementGraphValue);
            }

            graphValue = new GraphObjectValue(values);
            return true;
        }

        /// <summary>
        /// Attempt to resolve the given anonymous object to a GraphQL object.
        /// </summary>
        protected virtual bool TryGetAnonymousType(object objectValue, [NotNullWhen(true)] out IGraphValue? graphValue)
        {
            Dictionary<GraphName, IGraphValue> values = new();

            foreach(PropertyInfo propertyInfo in objectValue.GetType().GetProperties())
            {
                if(!this.TryResolveValue(propertyInfo.GetValue(objectValue), out IGraphValue? elementGraphValue))
                {
                    graphValue = null;
                    return false;
                }

                GraphName graphName = new(propertyInfo.Name);
                values.Add(graphName, elementGraphValue);
            }

            graphValue = new GraphObjectValue(values);
            return true;
        }
    }
}
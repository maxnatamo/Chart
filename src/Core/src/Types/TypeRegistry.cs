using System.Diagnostics.CodeAnalysis;

using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    /// <summary>
    /// Registry for handling and resolving runtime types to/from GraphQL schema types.
    /// </summary>
    public sealed partial class TypeRegistry
    {
        /// <summary>
        /// Hashmap of registered types in the registry.
        /// </summary>
        private readonly Dictionary<Type, Type> _registeredTypes;

        /// <summary>
        /// List of all visitied types, which may or may not be registered; avoids re-checking.
        /// </summary>
        private readonly List<Type> _visitedTypes;

        public TypeRegistry()
        {
            this._registeredTypes = new Dictionary<Type, Type>
            {
                { typeof(Boolean),  typeof(GraphBooleanValue) },
                { typeof(Single),   typeof(GraphFloatValue) },
                { typeof(Double),   typeof(GraphFloatValue) },
                { typeof(Decimal),  typeof(GraphFloatValue) },
                { typeof(SByte),    typeof(GraphIntValue) },
                { typeof(Int16),    typeof(GraphIntValue) },
                { typeof(Int32),    typeof(GraphIntValue) },
                { typeof(Int64),    typeof(GraphIntValue) },
                { typeof(Int128),   typeof(GraphIntValue) },
                { typeof(Byte),     typeof(GraphIntValue) },
                { typeof(UInt16),   typeof(GraphIntValue) },
                { typeof(UInt32),   typeof(GraphIntValue) },
                { typeof(UInt64),   typeof(GraphIntValue) },
                { typeof(UInt128),  typeof(GraphIntValue) },
                { typeof(String),   typeof(GraphStringValue) },
            };

            this._visitedTypes = this._registeredTypes.Select(t => t.Key).ToList();
        }

        /// <summary>
        /// Register a new type into the registry.
        /// Any descending types (method parameters, properties, etc.) are also registered, recursively.
        /// </summary>
        /// <param name="type">The type to register.</param>
        /// <returns>The current instance, to allow for method chaining.</returns>
        public TypeRegistry Register(Type type)
        {
            foreach(Type genericType in type.GetGenericArguments())
            {
                this.Register(genericType);
            }

            if(this._visitedTypes.Contains(type))
            {
                return this;
            }

            this._visitedTypes.Add(type);
            this.RegisterObjectType(type);

            return this;
        }

        /// <inheritdoc cref="TypeRegistry.Register(Type)" />
        /// <typeparam name="TRuntime">The type to register.</typeparam>
        public TypeRegistry Register<TRuntime>()
            => this.Register(typeof(TRuntime));

        /// <summary>
        /// Resolve a runtime type into a schema type.
        /// </summary>
        /// <param name="runtimeType">The runtime type to match into a schema type.</param>
        /// <returns>The schema type.</returns>
        /// <exception cref="KeyNotFoundException">The runtime type has not been registered.</exception>
        public Type ResolveType(Type runtimeType)
            => this._registeredTypes[runtimeType];

        /// <summary>
        /// Try to resolve a runtime type into a schema type.
        /// </summary>
        /// <param name="runtimeType">The runtime type to match into a schema type.</param>
        /// <param name="schemaType">Contains the schema type, if the runtime type is found. Otherwise, <c>null</c>.</param>
        /// <returns><c>true</c>, if the schema type was resolved. Otherwise, <c>false</c>.</returns>
        /// <exception cref="KeyNotFoundException">The runtime type has not been registered.</exception>
        public bool TryResolveType(Type runtimeType, [NotNullWhen(true)] out Type? schemaType)
            => this._registeredTypes.TryGetValue(runtimeType, out schemaType);

        /// <inheritdoc cref="TypeRegistry._registeredTypes" />
        public Dictionary<Type, Type> GetRegisteredTypes()
            => this._registeredTypes;

        /// <inheritdoc cref="TypeRegistry._visitedTypes" />
        public List<Type> GetVisitedTypes()
            => this._visitedTypes;

        /// <inheritdoc cref="TypeRegistry._visitedTypes" />
        public bool Visited(Type type)
            => this._visitedTypes.Contains(type);

        /// <inheritdoc cref="TypeRegistry._visitedTypes" />
        public bool Visited<T>()
            => this.Visited(typeof(T));
    }
}
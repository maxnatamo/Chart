using System.Diagnostics.CodeAnalysis;

namespace Chart.Core
{
    public interface ITypeRegistry
    {
        /// <summary>
        /// Map between type definition names (such as <c>Int</c>) to <see cref="TypeDefinition" />-instances.
        /// </summary>
        Dictionary<string, TypeDefinition> RuntimeTypes { get; }

        /// <summary>
        /// Map between type definition names (such as <c>specifiedBy</c>) to <see cref="TypeDefinition" />-instances.
        /// </summary>
        Dictionary<string, TypeDefinition> TypeDefinitionBindings { get; }

        /// <summary>
        /// Map between type definition names (such as <c>SpecifiedByDirective</c>) to <see cref="TypeDefinition" />-instances.
        /// </summary>
        Dictionary<string, TypeDefinition> TypeBindings { get; }

        /// <summary>
        /// Lookup of enum-definitions, keyed by the enum definitions name.
        /// </summary>
        Dictionary<string, List<string>> EnumDefinitions { get; }

        /// <summary>
        /// List of all registered types in the registry.
        /// </summary>
        Dictionary<string, RegisteredType> RegisteredTypes { get; }

        /// <summary>
        /// Try to get the registered type definition with the given name.
        /// </summary>
        bool TryGetType(string name, [NotNullWhen(true)] out RegisteredType? type);

        /// <summary>
        /// Whether the given type has been registered.
        /// </summary>
        bool Visited(string typeName);

        /// <inheritdoc cref="TypeRegistry.Visited(string)" />
        bool Visited(Type type);

        /// <inheritdoc cref="TypeRegistry.Visited(string)" />
        bool Visited<TType>();
    }

    /// <summary>
    /// Registry for containing GraphQL type definitions.
    /// </summary>
    public sealed partial class TypeRegistry : ITypeRegistry
    {
        /// <inheritdoc />
        public Dictionary<string, TypeDefinition> RuntimeTypes { get; } = new();

        /// <inheritdoc />
        public Dictionary<string, TypeDefinition> TypeDefinitionBindings { get; } = new();

        /// <inheritdoc />
        public Dictionary<string, TypeDefinition> TypeBindings { get; } = new();

        /// <inheritdoc />
        public Dictionary<string, List<string>> EnumDefinitions { get; } = new();

        public Dictionary<string, RegisteredType> RegisteredTypes { get; } = new();

        public TypeRegistry(IEnumerable<TypeDefinition> typeDefinitions)
        {
            this.PopulateDefinitions(typeDefinitions);
        }

        public bool TryGetType(string name, [NotNullWhen(true)] out RegisteredType? type)
        {
            foreach(KeyValuePair<string, RegisteredType> registeredType in this.RegisteredTypes)
            {
                if(registeredType.Value.IsOfType(name))
                {
                    type = registeredType.Value;
                    return true;
                }
            }

            type = null;
            return false;
        }

        public bool Visited(string typeName)
        {
            foreach(KeyValuePair<string, RegisteredType> type in this.RegisteredTypes)
            {
                if(type.Value.IsOfType(typeName))
                {
                    return true;
                }
            }

            // Halt type inheritence descending too deep, when registering types.
            return typeName == typeof(object).Name;
        }

        public bool Visited(Type type)
        {
            if(type.IsNonNullType() || type.IsListType())
            {
                return this.Visited(type.GetGenericArguments().First());
            }

            if(type.HasElementType)
            {
                Type elementalType = type.GetElementType()!;
                return this.Visited(elementalType);
            }

            return this.Visited(type.Name);
        }

        public bool Visited<TType>()
            => this.Visited(typeof(TType));

        private void PopulateDefinitions(IEnumerable<TypeDefinition> typeDefinitions)
        {
            foreach(TypeDefinition typeDefinition in typeDefinitions)
            {
                if(typeDefinition.Name is null)
                {
                    throw new InvalidOperationException(
                        $"Type {typeDefinition.GetType().Name} did not have a name associated. " +
                         "Set the Name property in it's constructor."
                    );
                }

                RegisteredType registeredTypetype = new(typeDefinition);
                this.RegisteredTypes.Add(registeredTypetype.SchemaName, registeredTypetype);
            }
        }
    }
}
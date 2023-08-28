namespace Chart.Core
{
    public interface ITypeRegistry
    {
        /// <summary>
        /// Map between type definition names (such as <c>Int</c>) to <see cref="ITypeDefinition" />-instances.
        /// </summary>
        Dictionary<string, ITypeDefinition> RuntimeTypes { get; }

        /// <summary>
        /// Map between type definition names (such as <c>specifiedBy</c>) to <see cref="ITypeDefinition" />-instances.
        /// </summary>
        Dictionary<string, ITypeDefinition> TypeDefinitionBindings { get; }

        /// <summary>
        /// Map between type definition names (such as <c>SpecifiedByDirective</c>) to <see cref="ITypeDefinition" />-instances.
        /// </summary>
        Dictionary<string, ITypeDefinition> TypeBindings { get; }

        /// <summary>
        /// Lookup of enum-definitions, keyed by the enum definitions name.
        /// </summary>
        Dictionary<string, List<string>> EnumDefinitions { get; }

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
        public Dictionary<string, ITypeDefinition> RuntimeTypes { get; } = new();

        /// <inheritdoc />
        public Dictionary<string, ITypeDefinition> TypeDefinitionBindings { get; } = new();

        /// <inheritdoc />
        public Dictionary<string, ITypeDefinition> TypeBindings { get; } = new();

        /// <inheritdoc />
        public Dictionary<string, List<string>> EnumDefinitions { get; } = new();

        public TypeRegistry(IEnumerable<ITypeDefinition> typeDefinitions)
        {
            this.PopulateDefinitions(typeDefinitions);
        }

        /// <summary>
        /// Whether the given type has been registered.
        /// </summary>
        public bool Visited(string typeName) =>
            this.RuntimeTypes.ContainsKey(typeName) ||
            this.TypeDefinitionBindings.ContainsKey(typeName) ||
            this.TypeBindings.ContainsKey(typeName) ||
            typeName == typeof(object).Name;

        /// <inheritdoc cref="TypeRegistry.Visited(string)" />
        public bool Visited(Type type)
        {
            if(type.IsNonNullType() || type.IsListType())
            {
                return this.Visited(type.GetGenericArguments().First());
            }

            if(type.HasElementType)
            {
                return this.Visited(type.GetElementType()!);
            }

            return this.Visited(type.Name);
        }

        /// <inheritdoc cref="TypeRegistry.Visited(string)" />
        public bool Visited<TType>()
            => this.Visited(typeof(TType));

        private void PopulateDefinitions(IEnumerable<ITypeDefinition> typeDefinitions)
        {
            foreach(ITypeDefinition typeDefinition in typeDefinitions)
            {
                // We need to register all the different names that a type can have:
                // - GraphQL schema name (such as 'Int'),
                // - definition type name (such as 'IntType'),
                // - and runtime type name (such as 'Int32'), if any

                if(typeDefinition.Name is null)
                {
                    throw new InvalidOperationException(
                        $"Type {typeDefinition.GetType().Name} did not have a name associated. " +
                         "Set the Name property in it's constructor."
                    );
                }


                this.TypeBindings.Add(typeDefinition.GetType().Name, typeDefinition);
                this.TypeDefinitionBindings.Add(typeDefinition.Name, typeDefinition);

                if(typeDefinition.RuntimeType is not null)
                {
                    this.RuntimeTypes.Add(typeDefinition.RuntimeType.Name, typeDefinition);
                }
            }
        }
    }
}
using System.Collections;
using System.Diagnostics.CodeAnalysis;

using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    /// <summary>
    /// Utility class for resolving (or attempting thereat) types from the <see cref="ITypeRegistry" /> into <see cref="GraphType" />- and <see cref="ITypeDefinition" />-instances.
    /// </summary>
    public interface ITypeResolver
    {
        /// <summary>
        /// Resolve the given native type (e.g. <see cref="Int32" />, <see cref="String" />, etc.) into a schema type.
        /// </summary>
        GraphType Resolve(Type runtimeType);

        /// <summary>
        /// Attempt to resolve the given native type (e.g. <see cref="Int32" />, <see cref="String" />, etc.) into a schema type.
        /// </summary>
        bool TryResolve(Type runtimeType, [NotNullWhen(true)] out GraphType? schemaType);

        /// <inheritdoc cref="ITypeResolver.Resolve(Type)" />
        GraphType Resolve<TRuntime>();

        /// <inheritdoc cref="ITypeResolver.TryResolve(Type, out GraphType?)" />
        bool TryResolve<TRuntime>([NotNullWhen(true)] out GraphType? schemaType);

        /// <summary>
        /// Resolve the given type definition (e.g. <see cref="StringType" />, <see cref="NonNullType{StringType}" />) into a schema type.
        /// </summary>
        GraphType ResolveDefinition(Type runtimeType);

        /// <summary>
        /// Attempt to resolve the given type definition (e.g. <see cref="StringType" />, <see cref="NonNullType{StringType}" />) into a schema type.
        /// </summary>
        bool TryResolveDefinition(Type runtimeType, [NotNullWhen(true)] out GraphType? schemaType);

        /// <inheritdoc cref="ITypeResolver.ResolveDefinition(Type)" />
        GraphType ResolveDefinition<TRuntime>();

        /// <inheritdoc cref="ITypeResolver.TryResolveDefinition(Type, out GraphType?)" />
        bool TryResolveDefinition<TRuntime>([NotNullWhen(true)] out GraphType? schemaType);

        /// <summary>
        /// Resolve the type definition, which corresponds to the given name.
        /// </summary>
        ITypeDefinition ResolveTypeDefinition(string typeName);

        /// <inheritdoc cref="ITypeResolver.ResolveTypeDefinition(string)" />
        TTypeDefinition ResolveTypeDefinition<TTypeDefinition>(string typeName)
            where TTypeDefinition : ITypeDefinition;

        /// <summary>
        /// Attempt to resolve the type definition, which corresponds to the given name.
        /// </summary>
        bool TryResolveTypeDefinition(string typeName, [NotNullWhen(true)] out ITypeDefinition? typeDefinition);

        /// <inheritdoc cref="ITypeResolver.TryResolveTypeDefinition(string, out ITypeDefinition?)" />
        bool TryResolveTypeDefinition<TTypeDefinition>(string typeName, [NotNullWhen(true)] out TTypeDefinition? typeDefinition)
            where TTypeDefinition : ITypeDefinition;

        /// <summary>
        /// Create a generic type of <see cref="ITypeDefinition" />, with the relevant composite types, inherited from the given type.
        /// </summary>
        Type CreateTypeDefinition(Type type);

        /// <summary>
        /// Create a generic type of <see cref="ITypeDefinition" />, with the relevant composite types, inherited from the given graph type.
        /// </summary>
        Type CreateTypeDefinition(GraphType type);
    }

    /// <summary>
    /// Utility class for checking compatibility between types and values.
    /// </summary>
    public class TypeResolver : ITypeResolver
    {
        private readonly ITypeRegistry _typeRegistry;

        public TypeResolver(ITypeRegistry typeRegistry)
            => this._typeRegistry = typeRegistry;

        /// <inheritdoc />
        public GraphType Resolve(Type runtimeType)
        {
            if(!this.TryResolve(runtimeType, out GraphType? schemaType))
            {
                throw new KeyNotFoundException($"Runtime type '{runtimeType.Name}' was not found.");
            }

            return schemaType;
        }

        /// <inheritdoc />
        public bool TryResolve(Type runtimeType, [NotNullWhen(true)] out GraphType? schemaType)
        {
            runtimeType = runtimeType.GetNullableType(out bool isNullable);

            if(runtimeType.IsGenericType && runtimeType.IsOfGenericType(typeof(IEnumerable)))
            {
                Type innerType = runtimeType.GetGenericArguments().First();

                if(!this.TryResolve(innerType, out GraphType? innerSchemaType))
                {
                    schemaType = null;
                    return false;
                }

                schemaType = new GraphListType(innerSchemaType)
                {
                    NonNullable = !isNullable
                };
                return true;
            }

            if(runtimeType.HasElementType && runtimeType.IsArray)
            {
                Type innerType = runtimeType.GetElementType()!;

                if(!this.TryResolve(innerType, out GraphType? innerSchemaType))
                {
                    schemaType = null;
                    return false;
                }

                schemaType = new GraphListType(innerSchemaType)
                {
                    NonNullable = !isNullable
                };
                return true;
            }

            if(this.TryResolveTypeDefinition(runtimeType.Name, out ITypeDefinition? value))
            {
                schemaType = new GraphNamedType(value.Name)
                {
                    NonNullable = !isNullable
                };
                return true;
            }

            schemaType = null;
            return false;
        }

        /// <inheritdoc />
        public GraphType Resolve<TRuntime>()
            => this.Resolve(typeof(TRuntime));

        /// <inheritdoc />
        public bool TryResolve<TRuntime>([NotNullWhen(true)] out GraphType? schemaType)
            => this.TryResolve(typeof(TRuntime), out schemaType);

        public GraphType ResolveDefinition(Type runtimeType)
        {
            if(!this.TryResolveDefinition(runtimeType, out GraphType? schemaType))
            {
                throw new KeyNotFoundException($"Runtime type definition '{runtimeType.Name}' was not found.");
            }

            return schemaType;
        }

        /// <inheritdoc />
        public bool TryResolveDefinition(Type runtimeType, [NotNullWhen(true)] out GraphType? schemaType)
        {
            bool isNonNullType = runtimeType.IsAssignableTo(typeof(INonNullType));
            bool isListType = runtimeType.IsAssignableTo(typeof(IListType));

            if(isNonNullType || isListType)
            {
                Type innerType = runtimeType.GetGenericArguments().First();

                if(!this.TryResolveDefinition(innerType, out GraphType? innerGraphType))
                {
                    schemaType = null;
                    return false;
                }

                innerGraphType = (GraphType) innerGraphType.Clone();

                if(isNonNullType)
                {
                    schemaType = innerGraphType;
                    schemaType.NonNullable = true;
                    return true;
                }

                if(isListType)
                {
                    schemaType = new GraphListType(innerGraphType);
                    return true;
                }
            }

            if(this._typeRegistry.TypeBindings.TryGetValue(runtimeType.Name, out ITypeDefinition? value))
            {
                schemaType = new GraphNamedType(value.Name)
                {
                    NonNullable = false
                };
                return true;
            }

            schemaType = null;
            return false;
        }

        /// <inheritdoc />
        public GraphType ResolveDefinition<TRuntime>()
            => this.ResolveDefinition(typeof(TRuntime));

        /// <inheritdoc />
        public bool TryResolveDefinition<TRuntime>([NotNullWhen(true)] out GraphType? schemaType)
            => this.TryResolveDefinition(typeof(TRuntime), out schemaType);

        /// <inheritdoc />
        public ITypeDefinition ResolveTypeDefinition(string typeName)
        {
            if(!this.TryResolveTypeDefinition(typeName, out ITypeDefinition? typeDefinition))
            {
                throw new KeyNotFoundException($"Type definition '{typeName}' was not found.");
            }

            return typeDefinition;
        }

        /// <inheritdoc />
        public TTypeDefinition ResolveTypeDefinition<TTypeDefinition>(string typeName)
            where TTypeDefinition : ITypeDefinition
            => (TTypeDefinition) this.ResolveTypeDefinition(typeName);

        /// <inheritdoc />
        public bool TryResolveTypeDefinition(string typeName, [NotNullWhen(true)] out ITypeDefinition? typeDefinition)
        {
            if(this._typeRegistry.TypeDefinitionBindings.TryGetValue(typeName, out typeDefinition))
            {
                return true;
            }

            if(this._typeRegistry.TypeBindings.TryGetValue(typeName, out typeDefinition))
            {
                return true;
            }

            if(this._typeRegistry.RuntimeTypes.TryGetValue(typeName, out typeDefinition))
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public bool TryResolveTypeDefinition<TTypeDefinition>(string typeName, [NotNullWhen(true)] out TTypeDefinition? typeDefinition)
            where TTypeDefinition : ITypeDefinition
        {
            if(!this.TryResolveTypeDefinition(typeName, out ITypeDefinition? resolvedDefinition))
            {
                typeDefinition = default;
                return false;
            }

            if(resolvedDefinition is not TTypeDefinition _resolvedDefinition)
            {
                typeDefinition = default;
                return false;
            }

            typeDefinition = _resolvedDefinition;
            return true;
        }

        /// <inheritdoc />
        public Type CreateTypeDefinition(Type type)
        {
            type = type.GetNullableType(out bool isNullable);

            if(!isNullable)
            {
                Type innerType = this.ResolveTypeDefinition(type.Name).GetType();
                return typeof(NonNullType<>).MakeGenericType(innerType);
            }

            if(type.IsGenericType && type.IsOfGenericType(typeof(IEnumerable)) && type.GetGenericArguments().Length > 0)
            {
                Type genericType = type.GetGenericArguments()[0];
                Type innerType = this.CreateTypeDefinition(genericType);

                return typeof(ListType<>).MakeGenericType(innerType);
            }

            return this.ResolveTypeDefinition(type.Name).GetType();
        }

        /// <inheritdoc />
        public Type CreateTypeDefinition(GraphType type)
        {
            if(type is GraphNamedType graphNamedType)
            {
                Type innerType = this.ResolveTypeDefinition(graphNamedType.Name).GetType();

                if(type.NonNullable)
                {
                    innerType = typeof(NonNullType<>).MakeGenericType(innerType);
                }

                return innerType;
            }

            if(type is GraphListType graphListType)
            {
                Type innerType = this.CreateTypeDefinition(graphListType.UnderlyingType);
                Type listType = typeof(ListType<>).MakeGenericType(innerType);

                if(type.NonNullable)
                {
                    listType = typeof(NonNullType<>).MakeGenericType(listType);
                }

                return listType;
            }

            throw new NotSupportedException();
        }
    }
}
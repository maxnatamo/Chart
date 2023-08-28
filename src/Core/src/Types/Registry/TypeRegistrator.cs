using System.Collections;
using System.Reflection;

using static System.Reflection.BindingFlags;

namespace Chart.Core
{
    public interface ITypeRegistrator
    {
        /// <summary>
        /// Register a new runtime type into the registry.
        /// Any descending types (method parameters, properties, etc.) are also registered, recursively.
        /// </summary>
        /// <param name="type">The type to register.</param>
        /// <param name="explicitName">An optional name override for the type.</param>
        /// <returns>The current instance, to allow for method chaining.</returns>
        TypeRegistrator Register(Type type, string? explicitName = null);

        /// <inheritdoc cref="TypeRegistrator.Register(Type, string?)" />
        /// <typeparam name="TRuntime">The type to register.</typeparam>
        TypeRegistrator Register<TRuntime>(string? explicitName = null);
    }

    /// <summary>
    /// Registry for handling and converting .NET native types to different parts of the GraphQL document.
    /// </summary>
    public class TypeRegistrator : ITypeRegistrator
    {
        private readonly ITypeRegistry _typeRegistry;
        private readonly ITypeCreator _typeCreator;
        private readonly INameFormatter _nameFormatter;

        public TypeRegistrator(
            ITypeRegistry typeRegistry,
            ITypeCreator typeCreator,
            INameFormatter nameFormatter)
        {
            this._typeRegistry = typeRegistry;
            this._typeCreator = typeCreator;
            this._nameFormatter = nameFormatter;
        }

        /// <inheritdoc />
        public TypeRegistrator Register(Type type, string? explicitName = null)
        {
            string typeKey = explicitName ?? type.Name;
            typeKey = typeKey.TrimEnd('&');

            if(type.IsGenericType)
            {
                this.RegisterGenericType(type);
                return this;
            }

            if(this._typeRegistry.Visited(type) || this._typeRegistry.Visited(typeKey))
            {
                return this;
            }

            // TODO: Dirty hack.
            // We need to register the object type, before we can use it in the type creator.
            // But, certain cases will make this loop infinitely, as the type has not yet been registered.
            this._typeRegistry.TypeDefinitionBindings.Add(typeKey, new BooleanType());

            if(!type.IsEnum)
            {
                this.RegisterObjectType(type);
            }

            this._typeRegistry.TypeDefinitionBindings[typeKey] = this._typeCreator.CreateTypeDefinition(type);

            return this;
        }

        /// <inheritdoc />
        public TypeRegistrator Register<TRuntime>(string? explicitName = null)
            => this.Register(typeof(TRuntime), explicitName);

        /// <summary>
        /// Recursively descend into a field and register any underlying types.
        /// </summary>
        private TypeRegistrator RegisterField(FieldInfo field)
            => this.Register(field.FieldType);

        /// <summary>
        /// Recursively descend into a property and register any underlying types.
        /// </summary>
        private TypeRegistrator RegisterProperty(PropertyInfo property)
            => this.Register(property.PropertyType);

        /// <summary>
        /// Recursively descend into a parameter and register any underlying types.
        /// </summary>
        private TypeRegistrator RegisterParameter(ParameterInfo parameter)
            => this.Register(parameter.ParameterType);

        /// <summary>
        /// Recursively descend into a method and register any underlying types.
        /// </summary>
        private TypeRegistrator RegisterMethod(MethodInfo method)
        {
            if(method.ReturnType != typeof(void))
            {
                this.Register(method.ReturnType);
            }

            foreach(Type genericType in method.GetGenericArguments())
            {
                this.Register(genericType);
            }

            foreach(ParameterInfo parameter in method.GetParameters())
            {
                this.RegisterParameter(parameter);
            }

            return this;
        }

        /// <summary>
        /// Recursively descend into a object and register any underlying types.
        /// </summary>
        private TypeRegistrator RegisterObjectType(Type objectType)
        {
            foreach(FieldInfo field in objectType.GetFields(Instance | Public))
            {
                this.RegisterField(field);
            }

            foreach(PropertyInfo property in objectType.GetProperties(Instance | Public))
            {
                this.RegisterProperty(property);
            }

            foreach(MethodInfo method in objectType.GetMethods(Public | DeclaredOnly | Instance))
            {
                // Ignore property getters/setters and operator overloads
                if(method.IsSpecialName)
                {
                    continue;
                }

                // Non-returning methods are not executable
                if(method.ReturnType == typeof(void))
                {
                    continue;
                }

                // Generic methods are currently not supported
                if(method.IsGenericMethod)
                {
                    continue;
                }

                this.RegisterMethod(method);
            }

            return this;
        }

        /// <summary>
        /// Register a generic type and it's generic arguments.
        /// </summary>
        private void RegisterGenericType(Type type)
        {
            if(!type.IsGenericType)
            {
                return;
            }

            foreach(Type genericType in type.GetGenericArguments())
            {
                this.Register(genericType);
            }

            // Lists are native to GraphQL, so no need to register them.
            if(type.IsAssignableTo(typeof(IEnumerable)))
            {
                return;
            }

            ITypeDefinition definition = this._typeCreator.CreateTypeDefinition(
                type,
                this._nameFormatter.GetGenericsName(type)
            );

            this._typeRegistry.TypeDefinitionBindings.Add(type.Name, definition);
        }
    }
}
using System.Reflection;

using Chart.Language.SyntaxTree;

using static System.Reflection.BindingFlags;

namespace Chart.Core
{
    public interface ITypeCreator
    {
        /// <summary>
        /// Create a new type definition, based on the given type.
        /// </summary>
        /// <param name="type">The type to base the type definition on.</param>
        /// <param name="typeName">Force the name of the newly created type. If <see langword="null" />, then the name will be inferred.</param>
        /// <remarks>
        /// If the given type, <paramref name="type" />, is a value type, the resulting type definition will be of type <see cref="ScalarType" />.
        /// Otherwise, it will of type <see cref="ObjectType" />.
        /// </remarks>
        ITypeDefinition CreateTypeDefinition(Type type, string? typeName = null);

        /// <inheritdoc cref="ITypeCreator.CreateTypeDefinition(Type, string?)" />
        ITypeDefinition CreateTypeDefinition<TType>(string? typeName = null);

        /// <summary>
        /// Create a definition from the given <see cref="GraphDefinition" />-instance.
        /// </summary>
        /// <param name="definition">The definition to create the definition from.</param>
        /// <returns>The created definition.</returns>
        ITypeDefinition CreateFromDefinition(GraphDefinition definition);
    }

    /// <summary>
    /// Utility class for creating type definitions.
    /// </summary>
    public partial class TypeCreator : ITypeCreator
    {
        private readonly INameFormatter _nameFormatter;
        private readonly ITypeResolver _typeResolver;
        private readonly IResolverCache _resolverCache;
        private readonly IValueRegistry _valueRegistry;

        public TypeCreator(
            INameFormatter nameFormatter,
            ITypeResolver typeResolver,
            IResolverCache resolverCache,
            IValueRegistry valueRegistry)
        {
            this._nameFormatter = nameFormatter;
            this._typeResolver = typeResolver;
            this._resolverCache = resolverCache;
            this._valueRegistry = valueRegistry;
        }

        /// <inheritdoc />
        public virtual ITypeDefinition CreateTypeDefinition(Type type, string? typeName = null)
        {
            string name = typeName ?? this.GetName(type);
            string? description = this.GetDescription(type);
            List<Directive>? directives = this.GetDirectives(type);

            return type switch
            {
                Type t when t.IsEnum => this.CreateEnumDefinition(name, description, type, directives),
                _ => this.CreateObjectDefinition(name, description, type, directives),
            };
        }

        /// <inheritdoc />
        public virtual ITypeDefinition CreateTypeDefinition<TType>(string? typeName = null)
            => this.CreateTypeDefinition(typeof(TType), typeName);

        /// <summary>
        /// Get the name of the given type.
        /// </summary>
        /// <remarks>
        /// If an explicit name is set (via <see cref="GraphNameAttribute" />), it is returned.
        /// Otherwise, the name is inferred from the type name. This inferred name will be formatted.
        /// </remarks>
        /// <param name="type">The type to get the name from.</param>
        /// <returns>The formatted type name.</returns>
        protected virtual GraphName GetName(
            ICustomAttributeProvider type,
            string? fallback = null,
            Func<string, string>? formatter = null)
        {
            formatter ??= this._nameFormatter.FormatTypeName;

            GraphName? explicitName = type.GetAttribute<GraphNameAttribute>()?.Name;
            if(explicitName is not null)
            {
                return explicitName;
            }

            string formattedTypeName = formatter(fallback ?? type.GetType().Name);
            return new GraphName(formattedTypeName);
        }

        /// <inheritdoc cref="TypeCreator.GetName(ICustomAttributeProvider, string?, Func{string, string}?)" />
        protected virtual GraphName GetName(Type type)
        {
            GraphName? explicitName = type.GetAttribute<GraphNameAttribute>()?.Name;
            if(explicitName is not null)
            {
                return explicitName;
            }

            string formattedTypeName = this._nameFormatter.GetGenericsName(type);
            return new GraphName(formattedTypeName);
        }

        /// <inheritdoc cref="TypeCreator.GetName(ICustomAttributeProvider, string?, Func{string, string}?)" />
        protected virtual GraphName GetName(MemberInfo member)
            => this.GetName(member, member.Name);

        /// <inheritdoc cref="TypeCreator.GetName(ICustomAttributeProvider, string?, Func{string, string}?)" />
        protected virtual GraphName GetName(ParameterInfo member)
            => this.GetName(member, member.Name);

        /// <summary>
        /// Attempt to get the description from the given type.
        /// </summary>
        /// <remarks>
        /// If an explicit description is set (via <see cref="GraphDescriptionAttribute" />), it is returned.
        /// Otherwise, returns <see langword="null" />.
        /// </remarks>
        /// <param name="type">The type to get the description from.</param>
        protected virtual GraphDescription? GetDescription(ICustomAttributeProvider type)
            => GraphDescription.From(type.GetAttribute<GraphDescriptionAttribute>()?.Description);

        /// <summary>
        /// Attempt to get the directives from the given type.
        /// </summary>
        /// <param name="type">The type to get the directives from.</param>
        protected virtual List<Directive>? GetDirectives(ICustomAttributeProvider type)
        {
            IEnumerable<GraphDirectiveAttribute> directives = type.GetAttributes<GraphDirectiveAttribute>();
            if(!directives.Any())
            {
                return null;
            }

            List<Directive> definition = new();

            foreach(GraphDirectiveAttribute attribute in directives)
            {
                Directive directive = new(attribute.Name);

                if(attribute.Arguments is not null)
                {
                    foreach(KeyValuePair<string, object?> argument in attribute.Arguments)
                    {
                        DirectiveArgumentDefinition argumentDefinition = new(
                            argument.Key,
                            this._valueRegistry.ResolveValue(argument.Value)
                        );

                        directive.Arguments.Add(argumentDefinition);
                    }
                }

                definition.Add(directive);
            }

            return definition;
        }

        /// <summary>
        /// Create an object type definitions with the given parameters.
        /// </summary>
        protected ITypeDefinition CreateObjectDefinition(
            string name,
            string? description,
            Type runtimeType,
            List<Directive>? directives)
        {
            ObjectType objectTypeDefinition = new(
                name: name,
                description: description,
                runtimeType: runtimeType,
                directives: directives
            );

            this.PopulateObjectTypeDefinition(runtimeType, objectTypeDefinition);

            return objectTypeDefinition;
        }

        /// <summary>
        /// Create an object type definitions with the given parameters.
        /// </summary>
        protected ITypeDefinition CreateEnumDefinition(
            string name,
            string? description,
            Type enumType,
            List<Directive>? directives)
        {
            EnumType enumTypeDefinition = new(
                name: name,
                description: description,
                runtimeType: enumType,
                directives: directives
            );

            this.PopulateEnumTypeDefinition(enumType, enumTypeDefinition);

            return enumTypeDefinition;
        }

        /// <summary>
        /// Populate the given object definition with fields, properties and methods from the given type.
        /// </summary>
        /// <param name="type">The type to inherit members from.</param>
        /// <param name="definition">The type definition to fill in.</param>
        protected virtual void PopulateObjectTypeDefinition(Type type, ObjectType definition)
        {
            foreach(FieldInfo field in type.GetFields(Instance | Public))
            {
                FieldDefinition fieldDefinition = this.CreateFieldDefinition(field);
                definition.Fields.Add(fieldDefinition);
            }

            foreach(PropertyInfo property in type.GetProperties(Instance | Public))
            {
                FieldDefinition fieldDefinition = this.CreateFieldDefinition(property);
                definition.Fields.Add(fieldDefinition);
            }

            foreach(MethodInfo method in type.GetMethods(Public | DeclaredOnly | Instance))
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

                FieldDefinition fieldDefinition = this.CreateFieldDefinition(method);
                definition.Fields.Add(fieldDefinition);
            }
        }

        /// <summary>
        /// Populate the given enum definition with values from the given type.
        /// </summary>
        /// <param name="type">The type to inherit values from.</param>
        /// <param name="definition">The type definition to fill in.</param>
        protected virtual void PopulateEnumTypeDefinition(Type type, EnumType definition)
        {
            foreach(object value in Enum.GetValues(type))
            {
                string valueName = value.ToString()!;
                MemberInfo valueMember = type.GetMember(value.ToString()!)[0];

                GraphName name = this.GetName(valueMember, valueName, this._nameFormatter.FormatEnumName);
                GraphDescription? description = this.GetDescription(valueMember);
                List<Directive>? directives = this.GetDirectives(valueMember);

                EnumValueType enumValueType = new(
                    name: name,
                    description: description,
                    runtimeType: valueMember.GetType(),
                    directives: directives
                );

                definition.Values.Add(enumValueType);
            }
        }

        /// <summary>
        /// Create a field definition from the given field.
        /// </summary>
        /// <param name="field">The field info to inherit from.</param>
        protected FieldDefinition CreateFieldDefinition(FieldInfo field) =>
            new(
                name: this.GetName(field),
                type: this._typeResolver.Resolve(field.FieldType),
                directives: this.GetDirectives(field)
            )
            {
                Description = this.GetDescription(field),
                Resolver = this._resolverCache.GetOrCompile(field)
            };

        /// <summary>
        /// Create a field definition from the given property.
        /// </summary>
        /// <param name="property">The property info to inherit from.</param>
        protected virtual FieldDefinition CreateFieldDefinition(PropertyInfo property) =>
            new(
                name: this.GetName(property),
                type: this._typeResolver.Resolve(property.PropertyType),
                directives: this.GetDirectives(property)
            )
            {
                Description = this.GetDescription(property),
                Resolver = this._resolverCache.GetOrCompile(property)
            };

        /// <summary>
        /// Create a field definition from the given method.
        /// </summary>
        /// <param name="method">The method info to inherit from.</param>
        protected virtual FieldDefinition CreateFieldDefinition(MethodInfo method)
        {
            FieldDefinition fieldDefinition = new(
                name: this.GetName(method),
                type: this._typeResolver.Resolve(method.ReturnType),
                directives: this.GetDirectives(method)
            )
            {
                Description = this.GetDescription(method),
                Resolver = this._resolverCache.GetOrCompile(method)
            };

            foreach(ParameterInfo parameter in method.GetParameters())
            {
                ArgumentDefinition argumentDefinition = this.CreateArgumentDefinition(parameter);
                fieldDefinition.Arguments.Add(argumentDefinition);
            }

            return fieldDefinition;
        }

        /// <summary>
        /// Create an argument definition from the given parameter.
        /// </summary>
        /// <param name="parameter">The parameter info to inherit from.</param>
        public ArgumentDefinition CreateArgumentDefinition(ParameterInfo parameter) =>
            new(
                name: this.GetName(parameter),
                type: this._typeResolver.CreateTypeDefinition(parameter.ParameterType),
                directives: this.GetDirectives(parameter)
            )
            {
                Description = this.GetDescription(parameter),
                DefaultValue = (parameter.IsOptional, parameter.DefaultValue) switch
                {
                    (true, object) => this._valueRegistry.ResolveValue(parameter.DefaultValue),
                    (true, null) => new GraphNullValue(),
                    (false, _) => null
                }
            };
    }
}
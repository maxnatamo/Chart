using Chart.Language;
using Chart.Language.SyntaxTree;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core
{
    public partial class SchemaBuilder
    {
        internal readonly RequestServiceBuilder ServiceBuilder;

        /// <summary>
        /// Query root operation type.
        /// </summary>
        public ObjectType? QueryType { get; private set; } = null;

        /// <summary>
        /// Mutation root operation type.
        /// </summary>
        public ObjectType? MutationType { get; private set; } = null;

        /// <summary>
        /// Subscription root operation type.
        /// </summary>
        public ObjectType? SubscriptionType { get; private set; } = null;

        /// <summary>
        /// Internal state for schema configurations, which contains definitions to be processed.
        /// </summary>
        /// <remarks>
        /// Collections in this class are not populated until after <see cref="SchemaConfigurations" /> has been executed.
        /// </remarks>
        internal sealed class SchemaConfigurationState
        {
            /// <summary>
            /// Type bindings from graph definition name to CLR type.
            /// </summary>
            /// <remarks>
            /// All types must inherit from <see cref="TypeDefinition" />.
            /// </remarks>
            internal readonly Dictionary<string, TypeDefinition> TypeBindings = new();

            /// <summary>
            /// List of all directive definitions to add to the schema.
            /// </summary>
            /// <remarks>
            /// All types must inherit from <see cref="DirectiveDefinition" />.
            /// </remarks>
            internal readonly Dictionary<string, DirectiveDefinition> DirectiveDefinitions = new();

            /// <summary>
            /// List of graph definitions, which were defined in the schema.
            /// </summary>
            internal readonly Dictionary<string, GraphDefinition> Definitions = new();
        }

        /// <summary>
        /// Collection of configurations to execute before the schema is to be built.
        /// </summary>
        internal readonly SchemaConfigurationCollection SchemaConfigurations = new();

        /// <inheritdoc cref="SchemaConfigurationState" />
        internal readonly SchemaConfigurationState ConfigurationState = new();

        internal SchemaBuilder(RequestServiceBuilder services)
            => this.ServiceBuilder = services;

        /// <summary>
        /// Add the following schema string to the builder.
        /// </summary>
        public SchemaBuilder FromSchema(string schema)
        {
            SchemaParser schemaParser = new();
            GraphDocument document = schemaParser.ParseSchema(schema);

            foreach(GraphDefinition graphDefinition in document.Definitions)
            {
                this.SchemaConfigurations.Add((builder, _) =>
                {
                    if(builder.ConfigurationState.Definitions.ContainsKey(graphDefinition.Name))
                    {
                        throw new ArgumentException(
                            $"Type of name '{graphDefinition.Name}' has already been defined.");
                    }

                    builder.ConfigurationState.Definitions.Add(graphDefinition.Name, graphDefinition);
                });
            }

            return this;
        }

        /// <summary>
        /// Add a configuration step to the schema builder.
        /// </summary>
        public SchemaBuilder Configure(SchemaConfigurationDelegate configurationDelegate)
        {
            this.SchemaConfigurations.Add(configurationDelegate);
            return this;
        }

        /// <inheritdoc cref="RequestServiceBuilder.ResetOptions()" />
        public SchemaBuilder ResetOptions()
        {
            this.ServiceBuilder.ResetOptions();
            return this;
        }

        /// <inheritdoc cref="RequestServiceBuilder.SetOptions(Action{GraphOptions})" />
        public SchemaBuilder SetOptions(Action<GraphOptions> configure)
        {
            this.ServiceBuilder.SetOptions(configure);
            return this;
        }

        /// <inheritdoc cref="RequestServiceBuilder.ModifyOptions(Action{GraphOptions})" />
        public SchemaBuilder ModifyOptions(Action<GraphOptions> configure)
        {
            this.ServiceBuilder.ModifyOptions(configure);
            return this;
        }

        /// <summary>
        /// Force the query type of the schema.
        /// </summary>
        public SchemaBuilder BindQuery<TType>()
        {
            this.BindRootType<TType>((builder, type) => builder.QueryType = type);
            return this;
        }

        /// <summary>
        /// Force the mutation type of the schema.
        /// </summary>
        public SchemaBuilder BindMutation<TType>()
        {
            this.BindRootType<TType>((builder, type) => builder.MutationType = type);
            return this;
        }

        /// <summary>
        /// Force the subscription type of the schema.
        /// </summary>
        public SchemaBuilder BindSubscription<TType>()
        {
            this.BindRootType<TType>((builder, type) => builder.SubscriptionType = type);
            return this;
        }

        /// <summary>
        /// Bind the given type parameter to a root type in the schema.
        /// </summary>
        /// <typeparam name="TType">The type to bind the root type to.</typeparam>
        /// <param name="handler">Action for assigning the root value to the type definition.</param>
        private void BindRootType<TType>(Action<SchemaBuilder, ObjectType> handler)
        {
            Type type = typeof(TType);
            this.AddType(type);

            this.SchemaConfigurations.Add((builder, provider) =>
            {
                IAttributeResolver attributeResolver = provider.GetRequiredService<IAttributeResolver>();
                string typeName = attributeResolver.GetExplicitName(type) ?? type.Name;

                ITypeResolver typeResolver = provider.GetRequiredService<ITypeResolver>();
                ObjectType rootType = typeResolver.ResolveTypeDefinition<ObjectType>(typeName);

                handler(builder, rootType);
            });
        }

        /// <summary>
        /// Bind a graph definition name to a C# native type, overriding any existing binds.
        /// </summary>
        /// <param name="clrType">The CLR type to bind to the given name.</param>
        /// <param name="typeName">The name of the graph definition to bind to.</param>
        /// <returns>The current instance, allowing for method chaining.</returns>
        /// <example>
        /// This code examples shows how to explicitly bind a CLR type to a named graph definition.
        /// <code language="cs">
        /// Schema.From(@"
        ///     type Query {
        ///         hero: Droid
        ///     }
        ///
        ///     type Droid {
        ///         id: String!
        ///         name: String!
        ///     }", c => c
        ///         // Explicitly binds the type Query to a graph definition of name "Query"
        ///         .AddType&lt;Query&gt;("Query")
        ///
        ///         // Binds the type Mutation to the inferred name of "Mutation"
        ///         .AddType&lt;Mutation&gt;()
        ///
        ///         // Explicitly binds the type ArticleSubscription to the name of "Subscription"
        ///         .AddType&lt;ArticleSubscription&gt;("Subscription"));
        /// </code>
        /// </example>
        public SchemaBuilder AddType(
            Type clrType,
            string? typeName = null,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            return clrType switch
            {
                Type when clrType.IsAssignableTo(typeof(DirectiveDefinition))
                    => this.AddDirectiveType(clrType, typeName, lifetime),

                Type when clrType.IsAssignableTo(typeof(TypeDefinition))
                    => this.AddDefinitionType(clrType, typeName, lifetime),

                Type when clrType.IsEnum && clrType.IsAssignableTo(typeof(System.Enum))
                    => this.AddNativeType(clrType, typeName, lifetime),

                Type when clrType.IsClass && !clrType.IsAssignableTo(typeof(Delegate))
                    => this.AddNativeType(clrType, typeName, lifetime),

                _ => throw new ArgumentException("Type must be a directive, definition or reference type.", nameof(clrType))
            };
        }

        /// <inheritdoc cref="SchemaBuilder.AddType(Type, string, ServiceLifetime)" />
        /// <typeparam name="T">
        /// <inheritdoc cref="SchemaBuilder.AddType" path="/param[@name='clrType']" />
        /// </typeparam>
        public SchemaBuilder AddType<T>(
            string? typeName = null,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
            where T : class
            => this.AddType(typeof(T), typeName, lifetime);

        /// <summary>
        /// Add a definition type to the builder.
        /// </summary>
        /// <param name="definitionType">The graph definition type to bind to the given name.</param>
        /// <param name="typeName">An explicit name to give to the definition.</param>
        private SchemaBuilder AddDefinitionType(Type definitionType, string? typeName, ServiceLifetime lifetime)
        {
            if(!definitionType.IsAssignableTo(typeof(TypeDefinition)))
            {
                throw new ArgumentException(
                    $"The definition type '{definitionType.Name}' does not inherit from TypeDefinition.",
                    nameof(definitionType));
            }

            this.RegisterService(definitionType, lifetime);
            this.RegisterService(typeof(TypeDefinition), definitionType, lifetime);

            this.SchemaConfigurations.Add((builder, provider) =>
            {
                TypeDefinition typeDefinition = (TypeDefinition) provider.GetRequiredService(definitionType);
                typeName ??= typeDefinition.Name;

                builder.ConfigurationState.TypeBindings.Add(typeName, typeDefinition);
            });

            return this;
        }

        /// <summary>
        /// Add a directive type to the builder.
        /// </summary>
        /// <param name="directiveType">The directive definition type to add.</param>
        private SchemaBuilder AddDirectiveType(Type directiveType, string? directiveName, ServiceLifetime lifetime)
        {
            if(!directiveType.IsAssignableTo(typeof(DirectiveDefinition)))
            {
                throw new ArgumentException(
                    $"The directive type '{directiveType.Name}' does not inherit from DirectiveDefinition.",
                    nameof(directiveType));
            }

            this.RegisterService(directiveType, lifetime);

            this.SchemaConfigurations.Add((builder, provider) =>
            {
                DirectiveDefinition directiveDefinition = (DirectiveDefinition) provider.GetRequiredService(directiveType);
                directiveName ??= directiveDefinition.Name;

                builder.ConfigurationState.DirectiveDefinitions.Add(directiveName, directiveDefinition);
            });

            return this;
        }

        /// <summary>
        /// Add a native C# type to the builder.
        /// </summary>
        /// <param name="clrType">The native type to bind to the given name.</param>
        /// <param name="typeName">An explicit name to give to the type.</param>
        private SchemaBuilder AddNativeType(
            Type clrType,
            string? typeName = null,
            ServiceLifetime lifetime = TypeDefinition.Scope)
        {
            this.RegisterService(clrType, lifetime);

            this.SchemaConfigurations.Add((builder, provider) =>
            {
                IAttributeResolver attributeResolver = provider.GetRequiredService<IAttributeResolver>();
                typeName ??= attributeResolver.GetExplicitName(clrType) ?? clrType.Name;

                if(builder.ConfigurationState.TypeBindings.ContainsKey(typeName))
                {
                    throw new ArgumentException($"Type of name '{typeName}' has already been defined.");
                }

                ITypeRegistrator typeRegistrator = provider.GetRequiredService<ITypeRegistrator>();
                typeRegistrator.Register(clrType, typeName);

                ITypeResolver typeResolver = provider.GetRequiredService<ITypeResolver>();
                TypeDefinition typeDefinition = typeResolver.ResolveTypeDefinition(typeName);

                builder.ConfigurationState.TypeBindings.Add(typeName, typeDefinition);
            });

            return this;
        }

        private void RegisterService(Type serviceType, ServiceLifetime lifetime) =>
            _ = lifetime switch
            {
                ServiceLifetime.Transient => this.ServiceBuilder.Services.AddTransient(serviceType),
                ServiceLifetime.Scoped => this.ServiceBuilder.Services.AddScoped(serviceType),
                ServiceLifetime.Singleton => this.ServiceBuilder.Services.AddSingleton(serviceType),

                _ => throw new NotSupportedException(lifetime.ToString())
            };

        private void RegisterService(Type serviceType, Type implementingType, ServiceLifetime lifetime) =>
            _ = lifetime switch
            {
                ServiceLifetime.Transient => this.ServiceBuilder.Services.AddTransient(serviceType, implementingType),
                ServiceLifetime.Scoped => this.ServiceBuilder.Services.AddScoped(serviceType, implementingType),
                ServiceLifetime.Singleton => this.ServiceBuilder.Services.AddSingleton(serviceType, implementingType),

                _ => throw new NotSupportedException(lifetime.ToString())
            };
    }
}
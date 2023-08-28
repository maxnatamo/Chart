using Chart.Language;
using Chart.Language.SyntaxTree;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core
{
    public class Schema : DefinitionCollectionBase
    {
        private readonly SchemaWriter _schemaWriter = new();
        internal readonly IServiceProvider ServiceProvider;

        /// <inheritdoc />
        public ObjectType QueryType { get; internal set; } = null!;

        /// <inheritdoc />
        public ObjectType? MutationType { get; internal set; } = null;

        /// <inheritdoc />
        public ObjectType? SubscriptionType { get; internal set; } = null;

        internal Schema(IServiceProvider provider)
            => this.ServiceProvider = provider;

        /// <summary>
        /// Create a new schema with the given content.
        /// </summary>
        /// <param name="schema">Schema content.</param>
        /// <param name="configure">Action for adding other definitions.</param>
        /// <returns>The complete schema.</returns>
        public static Schema From(string schema, Action<SchemaBuilder>? configure = null)
            => Schema.From(ServiceProviderHelper.DefaultCollection(), schema, configure);

        /// <inheritdoc cref="Schema.From(string, Action{SchemaBuilder}?)" />
        /// <param name="services">A common service builder for the schema.</param>
        public static Schema From(
            RequestServiceBuilder services,
            string schema,
            Action<SchemaBuilder>? configure = null) =>
            Schema.Create(
                services,
                c =>
                {
                    c.FromSchema(schema);
                    configure?.Invoke(c);
                });

        /// <inheritdoc cref="Schema.Create(RequestServiceBuilder, Action{SchemaBuilder})" />
        public static Schema Create(
            Action<SchemaBuilder> configure) =>
            Schema.Create(
                ServiceProviderHelper.DefaultCollection(),
                configure);

        /// <summary>
        /// Create a new schema with the given content.
        /// </summary>
        /// <param name="services">A common service builder for the schema.</param>
        /// <param name="configure">Action for adding definitions.</param>
        /// <returns>The complete schema.</returns>
        public static Schema Create(
            RequestServiceBuilder services,
            Action<SchemaBuilder> configure)
        {
            SchemaBuilder schemaBuilder = services.SchemaBuilder;
            configure?.Invoke(schemaBuilder);

            IServiceProvider provider = services.BuildServiceProvider();
            return provider.GetRequiredService<SchemaAccessor>().Schema;
        }

        /// <summary>
        /// Create an <see cref="IQueryExecutor" />-instance from the schema,
        /// allowing to execute queries against it.
        /// </summary>
        public IQueryExecutor MakeExecutable()
            => new QueryExecutor(this, this.ServiceProvider);

        /// <summary>
        /// Add the given graph definitions to the schema.
        /// </summary>
        internal void AddDefinitions(params GraphDefinition[] definitions)
        {
            foreach(GraphDefinition definition in definitions)
            {
                if(this.Definitions.Any(v => v.Name == definition.Name))
                {
                    throw new NotImplementedException(definition.Name);
                }

                this.Definitions.Add(definition);
            }
        }

        /// <summary>
        /// Convert the schema into SDL representation.
        /// </summary>
        public override string ToString() =>
            this._schemaWriter
                .Visit(this.CreateDocument())
                .ToString();

        /// <summary>
        /// Create a <see cref="GraphDocument"/>-instance, containing all the
        /// definitions in the schema.
        /// </summary>
        private GraphDocument CreateDocument()
        {
            GraphDocument document = new();

            if(!this.Definitions.Any(v => v.DefinitionKind == GraphDefinitionKind.Schema))
            {
                document.Definitions.Add(this.CreateSchemaDefinition());
            }

            document.Definitions.AddRange(this.Definitions);
            return document;
        }

        /// <summary>
        /// Create an explicit <c>schema</c> definition for the document.
        /// </summary>
        private GraphSchemaDefinition CreateSchemaDefinition()
        {
            ArgumentNullException.ThrowIfNull(this.QueryType, nameof(this.QueryType));
            ArgumentNullException.ThrowIfNull(this.QueryType.Name, nameof(this.QueryType.Name));

            GraphSchemaDefinition definition = new()
            {
                Values = new GraphSchemaValues()
                {
                    Values = new List<GraphSchemaValue>()
                    {
                        new GraphSchemaValue
                        {
                            Operation = new GraphName("query"),
                            Type = new GraphNamedType(this.QueryType.Name)
                        }
                    }
                }
            };

            if(this.MutationType is not null)
            {
                definition.Values.Values.Add(new()
                {
                    Operation = new GraphName("mutation"),
                    Type = new GraphNamedType(this.MutationType.Name)
                });
            }

            if(this.SubscriptionType is not null)
            {
                definition.Values.Values.Add(new()
                {
                    Operation = new GraphName("subscription"),
                    Type = new GraphNamedType(this.SubscriptionType.Name)
                });
            }

            return definition;
        }
    }
}
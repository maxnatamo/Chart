using Chart.Language.SyntaxTree;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using static System.StringComparison;

namespace Chart.Core
{
    public partial class SchemaBuilder
    {
        /// <summary>
        /// Build the final schema.
        /// </summary>
        public Schema Build(IServiceProvider serviceProvider)
        {
            GraphOptions options = serviceProvider.GetRequiredService<IOptions<GraphOptions>>().Value;
            bool addUnboundTypes = options.Validation.AddUnboundTypes;
            bool skipSchemaValidation = options.Validation.SkipSchemaValidation;

            ITypeCreator typeCreator = serviceProvider.GetRequiredService<ITypeCreator>();
            ITypeRegistry typeRegistry = serviceProvider.GetRequiredService<ITypeRegistry>();
            ITypeResolver typeResolver = serviceProvider.GetRequiredService<ITypeResolver>();
            ITypeRegistrator typeRegistrator = serviceProvider.GetRequiredService<ITypeRegistrator>();

            Schema schema = new(serviceProvider);

            this.SchemaConfigurations.Execute(this, serviceProvider);
            this.SchemaConfigurations.Clear();

            // Types which are registered with TypeRegistrator.Register are not added to TypeBindings.
            foreach(KeyValuePair<string, RegisteredType> registeredType in typeRegistry.RegisteredTypes)
            {
                if(this.ConfigurationState.TypeBindings.ContainsKey(registeredType.Key))
                {
                    continue;
                }

                this.ConfigurationState.TypeBindings.Add(registeredType.Key, registeredType.Value.TypeDefinition);
            }

            foreach(KeyValuePair<string, TypeDefinition> binding in this.ConfigurationState.TypeBindings)
            {
                // Ensure that a valid GraphDefinition exists for all type-bindings.
                if(!this.ConfigurationState.Definitions.TryGetValue(binding.Key, out GraphDefinition? bindingDefinition))
                {
                    bindingDefinition = binding.Value switch
                    {
                        ObjectType _typeDefinition => _typeDefinition.CreateSyntaxNode(serviceProvider),
                        ScalarType _typeDefinition => _typeDefinition.CreateSyntaxNode(serviceProvider),
                        EnumType _typeDefinition => _typeDefinition.CreateSyntaxNode(serviceProvider),
                        DirectiveDefinition _typeDefinition => _typeDefinition.CreateSyntaxNode(serviceProvider),

                        _ => throw new NotSupportedException(binding.Key)
                    };
                }

                this.ConfigurationState.Definitions.Remove(binding.Key);
                schema.AddDefinitions(bindingDefinition);
            }

            foreach(KeyValuePair<string, DirectiveDefinition> directiveType in this.ConfigurationState.DirectiveDefinitions)
            {
                GraphDefinition directiveGraphDefinition = directiveType.Value.CreateSyntaxNode(serviceProvider);

                this.ConfigurationState.Definitions.Remove(directiveType.Key);
                schema.AddDefinitions(directiveGraphDefinition);
            }

            if(addUnboundTypes)
            {
                GraphDefinition[] definitions = this.ConfigurationState.Definitions.Select(v => v.Value).ToArray();
                schema.AddDefinitions(definitions);
            }

            if(!skipSchemaValidation && this.ConfigurationState.Definitions.Any())
            {
                List<Error> errors = new();

                foreach(KeyValuePair<string, GraphDefinition> definition in this.ConfigurationState.Definitions)
                {
                    errors.Add(
                        new ErrorBuilder()
                            .SetMessage($"Schema definition '{definition.Key}' was not bound to a type.")
                            .Build());
                }

                throw new SchemaException(errors);
            }

            this.InferRootTypes();

            if(!skipSchemaValidation && this.QueryType is null)
            {
                throw new SchemaException(
                    new ErrorBuilder()
                        .SetMessage("No Query type was defined in the schema.")
                        .Build());
            }

            schema.QueryType = this.QueryType!;
            schema.MutationType = this.MutationType;
            schema.SubscriptionType = this.SubscriptionType;

            return schema;
        }

        private void InferRootTypes()
        {
            foreach(KeyValuePair<string, TypeDefinition> typeBinding in this.ConfigurationState.TypeBindings)
            {
                if(typeBinding.Value is not ObjectType objectType)
                {
                    continue;
                }

                switch(typeBinding.Key)
                {
                    case string key when key.Equals(Operations.Query, OrdinalIgnoreCase):
                        this.QueryType ??= objectType;
                        break;

                    case string key when key.Equals(Operations.Mutation, OrdinalIgnoreCase):
                        this.MutationType ??= objectType;
                        break;

                    case string key when key.Equals(Operations.Subscription, OrdinalIgnoreCase):
                        this.SubscriptionType ??= objectType;
                        break;
                }
            }
        }
    }
}
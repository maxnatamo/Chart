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

            ITypeDefinition CreateTypeDefinition(Type t, string name)
            {
                typeRegistrator.Register(t, name);
                return typeResolver.ResolveTypeDefinition(name);
            }

            Schema schema = new(serviceProvider);

            this.SchemaConfigurations.Execute(this, serviceProvider);
            this.SchemaConfigurations.Clear();

            // Types which are registered with TypeRegistrator.Register are not added to TypeBindings.
            foreach(KeyValuePair<string, ITypeDefinition> registeredType in typeRegistry.TypeDefinitionBindings)
            {
                if(this.ConfigurationState.TypeBindings.ContainsKey(registeredType.Key))
                {
                    continue;
                }

                this.ConfigurationState.TypeBindings.Add(registeredType.Key, registeredType.Value.GetType());
            }

            foreach(KeyValuePair<string, Type> binding in this.ConfigurationState.TypeBindings)
            {
                // Ensure that a valid GraphDefinition exists for all type-bindings.
                if(!this.ConfigurationState.Definitions.TryGetValue(binding.Key, out GraphDefinition? bindingDefinition))
                {
                    ITypeDefinition typeDefinition = binding.Value switch
                    {
                        Type t when t.IsTypeDefinition() => typeResolver.ResolveTypeDefinition(binding.Key),
                        Type t when t.IsClass() => CreateTypeDefinition(t, binding.Key),
                        Type t when t.IsEnum => CreateTypeDefinition(t, binding.Key),

                        Type t => throw new NotSupportedException(t.FullName)
                    };

                    bindingDefinition = typeDefinition switch
                    {
                        ObjectType _typeDefinition => _typeDefinition.CreateSyntaxNode(serviceProvider),
                        ScalarType _typeDefinition => _typeDefinition.CreateSyntaxNode(serviceProvider),
                        EnumType _typeDefinition => _typeDefinition.CreateSyntaxNode(serviceProvider),
                        DirectiveDefinition _typeDefinition => _typeDefinition.CreateSyntaxNode(serviceProvider),

                        _ => throw new NotSupportedException(typeDefinition.Name)
                    };
                }

                this.ConfigurationState.Definitions.Remove(binding.Key);
                schema.AddDefinitions(bindingDefinition);
            }

            foreach(Type directiveType in this.ConfigurationState.DirectiveDefinitions)
            {
                DirectiveDefinition? directiveDefinition = (DirectiveDefinition?) serviceProvider.GetService(directiveType)
                    ?? throw new NotImplementedException();

                GraphDefinition directiveGraphDefinition = directiveDefinition.CreateSyntaxNode(serviceProvider);

                this.ConfigurationState.Definitions.Remove(directiveDefinition.Name);
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

            if(this.QueryType is not null)
            {
                schema.QueryType = (ObjectType) typeCreator.CreateTypeDefinition(this.QueryType);
            }

            if(this.MutationType is not null)
            {
                schema.MutationType = (ObjectType) typeCreator.CreateTypeDefinition(this.MutationType);
            }

            if(this.SubscriptionType is not null)
            {
                schema.SubscriptionType = (ObjectType) typeCreator.CreateTypeDefinition(this.SubscriptionType);
            }

            return schema;
        }

        private void InferRootTypes()
        {
            foreach(KeyValuePair<string, Type> typeBinding in this.ConfigurationState.TypeBindings)
            {
                switch(typeBinding.Key)
                {
                    case string key when key.Equals(Operations.Query, OrdinalIgnoreCase):
                        this.QueryType ??= typeBinding.Value;
                        break;

                    case string key when key.Equals(Operations.Mutation, OrdinalIgnoreCase):
                        this.MutationType ??= typeBinding.Value;
                        break;

                    case string key when key.Equals(Operations.Subscription, OrdinalIgnoreCase):
                        this.SubscriptionType ??= typeBinding.Value;
                        break;
                }
            }
        }
    }
}
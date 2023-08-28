using Chart.Language.SyntaxTree;
using Chart.Utilities;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core.Validation
{
    public abstract class SchemaValidationTestBase
    {
        protected readonly IValidationRule Rule;

        protected SchemaValidationTestBase(Action<RequestServiceBuilder> configure)
        {
            RequestServiceBuilder services = new RequestServiceBuilder()
                .ModifyOptions(v => v.Validation.SkipSchemaValidation = true)
                .AddValidationCore();

            configure(services);

            IServiceProvider provider = services.Services.BuildServiceProvider();

            this.Rule = provider.GetRequiredService<IValidationRule>();
        }

        private SchemaValidationContext CreateContext(Schema schema)
            => new(schema);

        protected RequestServiceBuilder CreateServiceCollection() =>
            new RequestServiceBuilder()
                .ModifyOptions(v =>
                {
                    v.Validation.SkipSchemaValidation = true;
                    v.Validation.AddUnboundTypes = true;
                })
                .AddValidationCore()
                .AddTypeServices()
                .AddUtility();

        private async Task<IEnumerable<Error>> ExpectBaseAsync(SchemaSourceString sourceString)
        {
            // Arrange
            Schema schema = Schema.From(
                this.CreateServiceCollection(),
                sourceString.Schema);

            SchemaValidationContext context = this.CreateContext(schema);
            IValidationVisitor visitor = this.Rule.CreateVisitor();

            DocumentTraverser documentTraverser = new();

            // Act
            IEnumerable<GraphNodeInfo> nodes = documentTraverser.Traverse(schema.Definitions);
            await this.IterateNodesAsync(nodes, async node => await visitor.EnterAsync(node, context));
            await this.IterateNodesAsync(nodes, async node => await visitor.LeaveAsync(node, context));

            // Assert
            return context.Errors;
        }

        protected async Task ExpectNoErrorsAsync(SchemaSourceString sourceString)
        {
            IEnumerable<Error> errors = await this.ExpectBaseAsync(sourceString);
            errors.Should().BeEmpty();
        }

        protected async Task ExpectErrorsAsync(
            SchemaSourceString sourceString,
            params Action<Error>[] assertions)
        {
            IEnumerable<Error> errors = await this.ExpectBaseAsync(sourceString);
            errors.Should().SatisfyRespectively(assertions);
        }

        protected async Task IterateNodesAsync(IEnumerable<GraphNodeInfo> nodes, Func<IGraphNode, Task> action)
        {
            foreach(GraphNodeInfo node in nodes)
            {
                await action(node.Node);
            }
        }
    }
}
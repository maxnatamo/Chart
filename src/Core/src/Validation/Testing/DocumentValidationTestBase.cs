using Chart.Language.SyntaxTree;
using Chart.Utilities;

using FluentAssertions;

namespace Chart.Core.Validation
{
    public abstract class DocumentValidationTestBase : SchemaValidationTestBase
    {
        protected DocumentValidationTestBase(Action<RequestServiceBuilder> configure)
            : base(configure)
        { }

        private async Task<IEnumerable<Error>> ExpectBaseAsync(DocumentSourceString sourceString)
        {
            // Arrange
            Schema schema = Schema.From(
                this.CreateServiceCollection(),
                sourceString.Schema);

            QueryRequest query = new QueryRequestBuilder()
                .SetQuery(sourceString.Query)
                .Create(parse: true);

            DocumentValidationContext context = new(schema, query);
            IValidationVisitor visitor = this.Rule.CreateVisitor();

            DocumentTraverser documentTraverser = new();

            // Act
            IEnumerable<GraphNodeInfo> schemaNodes = documentTraverser.Traverse(schema.Definitions);
            await this.IterateNodesAsync(schemaNodes, async node => await visitor.EnterAsync(node, context));
            await this.IterateNodesAsync(schemaNodes, async node => await visitor.LeaveAsync(node, context));

            IEnumerable<GraphNodeInfo> queryNodes = documentTraverser.Traverse(query.Definitions);
            await this.IterateNodesAsync(queryNodes, async node => await visitor.EnterAsync(node, context));
            await this.IterateNodesAsync(queryNodes, async node => await visitor.LeaveAsync(node, context));

            // Assert
            return context.Errors;
        }

        protected async Task ExpectNoErrorsAsync(DocumentSourceString sourceString)
        {
            IEnumerable<Error> errors = await this.ExpectBaseAsync(sourceString);
            errors.Should().BeEmpty();
        }

        protected async Task ExpectErrorsAsync(
            DocumentSourceString sourceString,
            params Action<Error>[] assertions)
        {
            IEnumerable<Error> errors = await this.ExpectBaseAsync(sourceString);
            errors.Should().SatisfyRespectively(assertions);
        }
    }
}
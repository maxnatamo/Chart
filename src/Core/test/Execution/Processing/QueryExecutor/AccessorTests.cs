using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core.Tests.Execution.Processing.QueryExecutorTests
{
    public class AccessorTests
    {
        [Fact]
        public async Task ExecuteAsync_UpdatesQuery()
        {
            // Arrange
            Schema schema = Schema
                .Create(c => c.AddType<TestClass>("Query"));

            IQueryExecutor executor = schema.MakeExecutable();

            QueryRequest request = new QueryRequestBuilder()
                .SetQuery("{ get }")
                .Create();

            // Act
            await executor.ExecuteAsync(request);

            // Assert
            schema.ServiceProvider.GetRequiredService<QueryRequestAccessor>().Request
                .Should().BeEquivalentTo(request);
        }

        [Fact]
        public async Task ExecuteAsync_UpdatesSchema()
        {
            // Arrange
            Schema schema = Schema
                .Create(c => c.AddType<TestClass>("Query"));

            IQueryExecutor executor = schema.MakeExecutable();

            QueryRequest request = new QueryRequestBuilder()
                .SetQuery("{ get }")
                .Create();

            // Act
            await executor.ExecuteAsync(request);

            // Assert
            schema.ServiceProvider.GetRequiredService<SchemaAccessor>().Schema
                .Should().BeEquivalentTo(schema);
        }

        [Fact]
        public async Task ExecuteAsync_UpdatesResult()
        {
            // Arrange
            Schema schema = Schema
                .Create(c => c.AddType<TestClass>("Query"));

            IQueryExecutor executor = schema.MakeExecutable();

            QueryRequest request = new QueryRequestBuilder()
                .SetQuery("{ get }")
                .Create();

            // Act
            ExecutionResult result = await executor.ExecuteAsync(request);

            // Assert
            schema.ServiceProvider.GetRequiredService<ExecutionResultAccessor>().Result
                .Should().BeEquivalentTo(result);
        }

        private class TestClass
        {
            public string Get() => string.Empty;

            public string Get(string input) => string.Empty;
        }
    }
}
namespace Chart.Core.Tests.Execution.Processing.QueryExecutorTests
{
    public class ExecuteAsyncTests
    {
        [Fact]
        public async Task ExecuteAsync_ThrowsNullException_GivenEmptyStringRequest()
        {
            // Arrange
            IQueryExecutor executor =
                Schema
                    .Create(c => c.AddType<StringQuery>("Query"))
                    .MakeExecutable();

            // Act
            Func<Task> act = () => executor.ExecuteAsync(requestQuery: string.Empty);

            // Assert
            await act.Should().ThrowExactlyAsync<ArgumentException>();
        }

        [Fact]
        public async Task ExecuteAsync_ThrowsNullException_GivenNullRequest()
        {
            // Arrange
            IQueryExecutor executor =
                Schema
                    .Create(c => c.AddType<StringQuery>("Query"))
                    .MakeExecutable();

            // Act
            Func<Task> act = () => executor.ExecuteAsync(request: null!);

            // Assert
            await act.Should().ThrowExactlyAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task ExecuteAsync_ReturnsOperationCanceledException_GivenCancellingRequest()
        {
            // Arrange
            // Act
            ExecutionResult result =
                await Schema
                    .Create(c => c.AddType<Query>())
                    .MakeExecutable()
                    .ExecuteAsync("{ name }", Query.CancellationTokenSource.Token);

            // Assert
            result.Should().MatchSnapshot();
        }

        [Fact]
        public async Task ExecuteAsync_ReturnsCorrectStringValue()
        {
            // Arrange
            // Act
            ExecutionResult result =
                await Schema
                    .Create(c => c.AddType<StringQuery>("Query"))
                    .MakeExecutable()
                    .ExecuteAsync("{ name }");

            // Assert
            result.MatchSnapshot();
        }

        private class Query
        {
            public static CancellationTokenSource CancellationTokenSource = new();

            public string GetName()
            {
                Query.CancellationTokenSource.Cancel();
                return "Steve";
            }
        }
    }
}
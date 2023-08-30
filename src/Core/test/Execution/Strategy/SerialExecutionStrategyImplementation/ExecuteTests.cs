using Chart.Core.Execution;

using FluentAssertions.Extensions;

namespace Chart.Core.Tests.Execution.Strategy.SerialExecutionStrategyImplementationTests
{
    public class ExecuteTests
    {
        [Fact]
        public async Task ExecutesTask()
        {
            // Arrange
            bool state = false;
            Func<Task> task = () =>
            {
                state = true;
                return Task.CompletedTask;
            };

            SerialExecutionStrategyImplementation strategy = new();

            strategy.AddTask(task);

            // Act
            await strategy.Execute();

            // Assert
            state.Should().BeTrue();
        }

        [Fact]
        public async Task ExecutesAllTask()
        {
            // Arrange
            int index = 0;
            Func<Task> task = () =>
            {
                Interlocked.Increment(ref index);
                return Task.CompletedTask;
            };

            SerialExecutionStrategyImplementation strategy = new();

            strategy.AddTask(task);
            strategy.AddTask(task);
            strategy.AddTask(task);

            // Act
            await strategy.Execute();

            // Assert
            index.Should().Be(3);
        }

        [Fact]
        public async Task ExecutesAllTaskInSequal()
        {
            // Arrange
            Func<Task> task = () => Task.Delay(100);
            SerialExecutionStrategyImplementation strategy = new();

            strategy.AddTask(task);
            strategy.AddTask(task);
            strategy.AddTask(task);

            // Act
            Func<Task> act = () => strategy.Execute();

            // Assert
            await act.Should().NotCompleteWithinAsync(250.Milliseconds(), "serial tasks of 100ms should take at least 300ms to complete");
        }

        [Fact]
        public async Task ReturnsValues()
        {
            // Arrange
            SerialExecutionStrategyImplementation strategy = new();

            for(int i = 0; i < 5; i++)
            {
                int _i = i;
                strategy.AddTask(() => Task.FromResult(_i));
            }

            // Act
            IEnumerable<int> result = await strategy.ExecuteResult<int>();

            // Assert
            result.Should().HaveCount(5);
            result.Should().Contain(0);
            result.Should().Contain(1);
            result.Should().Contain(2);
            result.Should().Contain(3);
            result.Should().Contain(4);
        }

        [Fact]
        public async Task ReturnsValuesInCorrectOrder()
        {
            // Arrange
            SerialExecutionStrategyImplementation strategy = new();

            for(int i = 0; i < 5; i++)
            {
                int _i = i;
                strategy.AddTask(() => Task.FromResult(_i));
            }

            // Act
            IEnumerable<int> result = await strategy.ExecuteResult<int>();

            // Assert
            result.Should().HaveCount(5);
            result.Should().BeInAscendingOrder();
        }

        [Fact]
        public async Task ExecutesTasksWithCorrectType()
        {
            // Arrange
            SerialExecutionStrategyImplementation strategy = new();

            strategy.AddTask(() => Task.FromResult(3));
            strategy.AddTask(() => Task.FromResult("test"));

            // Act
            IEnumerable<int> result = await strategy.ExecuteResult<int>();

            // Assert
            result.Should().HaveCount(1);
            result.Should().Contain(3);
        }
    }
}
using Chart.Core.Execution;

namespace Chart.Core.Tests.Execution.Strategy.DefaultExecutionStrategySelectorTests
{
    public class SelectTests
    {
        [Theory]
        [InlineData(ExecutionStrategy.Serial)]
        [InlineData(ExecutionStrategy.Parallel)]
        public void Select_DoesntThrowException_GivenDefaultStrategies(ExecutionStrategy strategy)
        {
            // Arrange
            DefaultExecutionStrategySelector selector = new();

            // Act
            Action act = () => selector.Select(strategy);

            // Assert
            act.Should().NotThrow();
        }
    }
}
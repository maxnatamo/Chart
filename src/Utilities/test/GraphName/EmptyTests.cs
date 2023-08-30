namespace Chart.Utilities.Tests.GraphNameTests
{
    public class EmptyTests
    {
        [Fact]
        public void Empty_ShouldNotThrow()
        {
            // Arrange

            // Act
            Action act = () => _ = GraphName.Empty;

            // Assert
            act.Should().NotThrow<Exception>();
        }

        [Fact]
        public void Empty_ShouldHaveEmptyValue()
        {
            // Arrange

            // Act
            GraphName name = GraphName.Empty;

            // Assert
            name.Value.Should().Be(string.Empty);
        }
    }
}
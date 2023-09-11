namespace Chart.Utilities.Tests.GraphNameTests
{
    public class GraphNameTests
    {
        [Fact]
        public void GraphName_ShouldThrow_GivenEmptyString()
        {
            // Arrange
            string name = string.Empty;

            // Act
            Action act = () => _ = new GraphName(name);

            // Assert
            act.Should().Throw<Exception>("NameStart is required by the spec");
        }

        [Fact]
        public void GraphName_ShouldNotThrow_GivenSingleUnderscore()
        {
            // Arrange
            string name = "_";

            // Act
            Action act = () => _ = new GraphName(name);

            // Assert
            act.Should().NotThrow<Exception>();
        }

        [Fact]
        public void GraphName_ShouldThrow_GivenNameWithLeadingInteger()
        {
            // Arrange
            string name = "1Test";

            // Act
            Action act = () => _ = new GraphName(name);

            // Assert
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void GraphName_ShouldNotThrow_GivenNameWithLeadingLetter()
        {
            // Arrange
            string name = "Test";

            // Act
            Action act = () => _ = new GraphName(name);

            // Assert
            act.Should().NotThrow<Exception>();
        }
    }
}
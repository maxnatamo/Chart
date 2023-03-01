namespace Chart.Core.Parsers.Tests
{
    public class DocumentTests
    {
        [Fact]
        public void EmptyStringReturnsEmptyDocument()
        {
            // Arrange
            string source = "";

            // Act
            GraphDocument document = new SchemaParser().Parse(source);

            // Assert
            document.Should().NotBeNull();
            document.Definitions.Should().BeEmpty();
            document.Source.Should().Be("");
        }

        [Fact]
        public void EmptyBracesThrowsEmptySelectionException()
        {
            // Arrange
            string source = "{ }";

            // Act
            Action act = () => new SchemaParser().Parse(source);

            // Assert
            act.Should().Throw<EmptySelectionException>();
        }
    }
}
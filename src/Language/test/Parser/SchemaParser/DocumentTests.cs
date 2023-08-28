namespace Chart.Language.Tests
{
    public class DocumentTests
    {
        [Fact]
        public void EmptyStringReturnsEmptyDocument()
        {
            // Arrange
            string source = "";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

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
            Action act = () => new SchemaParser().ParseQuery(source);

            // Assert
            act.Should().Throw<EmptySelectionException>();
        }
    }
}
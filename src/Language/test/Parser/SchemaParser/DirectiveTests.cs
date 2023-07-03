namespace Chart.Language.Parsers.Tests
{
    public class DirectiveTests
    {
        [Fact]
        public void EmptyDirectiveThrowsEmptyDirectiveException()
        {
            // Arrange
            string source = "directive @TEST on";

            // Act
            Action act = () => new SchemaParser().Parse(source);

            // Assert
            act.Should().Throw<EmptyDirectiveException>();
        }

        [Fact]
        public void DirectiveWithSingleValidLocationReturnsDirective()
        {
            // Arrange
            string source = "directive @Test on SCHEMA";

            // Act
            GraphDocument document = new SchemaParser().Parse(source);

            // Assert
            document.Definitions[0]
                .As<GraphDirectiveDefinition>().Name?.Value.Should().Be("Test");

            document.Definitions[0]
                .As<GraphDirectiveDefinition>().Locations.Locations.Should().NotBeEmpty();

            document.Definitions[0]
                .As<GraphDirectiveDefinition>().Locations.Locations[0].Value
                .Should().Be("SCHEMA");
        }
    }
}
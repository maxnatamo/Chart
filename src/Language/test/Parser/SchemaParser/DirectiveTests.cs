namespace Chart.Language.Tests
{
    public class DirectiveTests
    {
        [Fact]
        public void EmptyDirectiveThrowsEmptyDirectiveException()
        {
            // Arrange
            string source = "directive @TEST on";

            // Act
            Action act = () => new SchemaParser().ParseSchema(source);

            // Assert
            act.Should().Throw<EmptyDirectiveException>();
        }

        [Fact]
        public void DirectiveWithSingleValidLocationReturnsDirective()
        {
            // Arrange
            string source = "directive @Test on SCHEMA";

            // Act
            GraphDocument document = new SchemaParser().ParseSchema(source);

            // Assert
            document.Definitions[0]
                .As<GraphDirectiveDefinition>().Name?.Value.Should().Be("Test");

            document.Definitions[0]
                .As<GraphDirectiveDefinition>().Locations.Locations.Should().Be(GraphDirectiveLocationFlags.SCHEMA);
        }
    }
}
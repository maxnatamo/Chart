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

        [Fact]
        public void FieldWithDirective_ReturnsDirective()
        {
            // Arrange
            string source = @"query { user @authorize }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Name.Value.Should().Be("user");

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Directives.Should().NotBeNull();

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Directives!.Directives.Should().HaveCount(1);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Directives!.Directives[0]
                .Name.Value.Should().Be("authorize");
        }
    }
}
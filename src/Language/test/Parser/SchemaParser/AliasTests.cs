namespace Chart.Language.Tests
{
    public class AliasTests
    {
        [Fact]
        public void NoAliasReturnsNullAlias()
        {
            // Arrange
            string source = @"query { user }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Should().NotBeNull();
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Alias.Should().BeNull();
        }

        [Fact]
        public void AliasedFieldReturnsFieldWithAlias()
        {
            // Arrange
            string source = @"query { alias: user }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Should().NotBeNull();
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Alias?.Value.Should().Be("alias");
        }

        [Fact]
        public void AliasedSelectionSetReturnsSelectionSetWithAlias()
        {
            // Arrange
            string source = @"query { alias: user { name } }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Should().NotBeNull();
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Alias?.Value.Should().Be("alias");
        }
    }
}
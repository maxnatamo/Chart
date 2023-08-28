namespace Chart.Language.Tests
{
    public class SelectionTests
    {
        [Fact]
        public void SingleSelectionReturnsSingleField()
        {
            // Arrange
            string source = "{ field }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Should().NotBeNull();
            document.Definitions.Should().NotBeEmpty();
            document.Definitions[0].DefinitionKind.Should().Be(GraphDefinitionKind.Operation);
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0].SelectionKind
                .Should().Be(GraphSelectionKind.Field);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Name.Value.Should().Be("field");

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().SelectionSet.Should().BeNull();
        }

        [Fact]
        public void NestedSelectionReturnsNestedField()
        {
            // Arrange
            string source = "{ user { email password } }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Should().NotBeNull();
            document.Definitions.Should().NotBeEmpty();
            document.Definitions[0].DefinitionKind.Should().Be(GraphDefinitionKind.Operation);
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0].SelectionKind
                .Should().Be(GraphSelectionKind.Field);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Name.Value
                .Should().Be("user");

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().SelectionSet.Should().NotBeNull();

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().SelectionSet?.Selections[0]
                .As<GraphFieldSelection>().Name.Value.Should().Be("email");

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().SelectionSet?.Selections[1]
                .As<GraphFieldSelection>().Name.Value.Should().Be("password");
        }
    }
}
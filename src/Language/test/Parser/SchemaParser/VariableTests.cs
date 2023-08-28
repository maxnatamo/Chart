namespace Chart.Language.Tests
{
    public class VariableTests
    {
        [Fact]
        public void EmptyVariableListReturnsEmptyVariable()
        {
            // Arrange
            string source = @"query() { user }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Variables?.Variables.Should().BeEmpty();
        }

        [Fact]
        public void SingleIntegerVariableReturnsNamedVariable()
        {
            // Arrange
            string source = @"query($id: Int) { user }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Variables?.Variables[0].Name
                .Value.Should().Be("id");

            document.Definitions[0]
                .As<GraphOperationDefinition>().Variables?.Variables[0].Type
                .As<GraphNamedType>().Name.Value.Should().Be("Int");

            document.Definitions[0]
                .As<GraphOperationDefinition>().Variables?.Variables[0].Type
                .As<GraphNamedType>().NonNullable.Should().BeFalse();
        }

        [Fact]
        public void SingleIntegerVariableWithDefaultValueReturnsVariableWithDefaultValue()
        {
            // Arrange
            string source = @"query($id: Int = 1337) { user }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Variables?.Variables[0]
                .DefaultValue.Should().NotBeNull();

            document.Definitions[0]
                .As<GraphOperationDefinition>().Variables?.Variables[0]
                .As<GraphVariable>().DefaultValue
                .As<GraphIntValue>().Value.Should().Be(1337);
        }
    }
}
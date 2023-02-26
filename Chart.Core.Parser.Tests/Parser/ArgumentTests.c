namespace Chart.Core.Parser.Tests
{
    public class ArgumentTests
    {
        [Fact]
        public void EmptyArgumentListReturnsEmptyArguments()
        {
            // Arrange
            string source = @"query { user() }";

            // Act
            GraphDocument document = new Parser().Parse(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments.Should().NotBeNull();

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments.Should().BeEmpty();
        }

        [Fact]
        public void SingleIntegerArgumentReturnsTypeArgument()
        {
            // Arrange
            string source = @"query { user(id: 1) }";

            // Act
            GraphDocument document = new Parser().Parse(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .ArgumentKind.Should().Be(GraphArgumentKind.Type);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .As<GraphTypeArgument>().Value
                .As<GraphStringValue>().Value.Should().Be("1");
        }

        [Fact]
        public void SingleVariableArgumentReturnsVariableArgument()
        {
            // Arrange
            string source = @"query { user(id: $variable) }";

            // Act
            GraphDocument document = new Parser().Parse(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .ArgumentKind.Should().Be(GraphArgumentKind.Variable);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .As<GraphVariableArgument>().Variable.Value.Should().Be("variable");
        }

        [Fact]
        public void ListArgumentWithNoElementReturnsListArgument()
        {
            // Arrange
            string source = @"query { user(id: []) }";

            // Act
            GraphDocument document = new Parser().Parse(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .ArgumentKind.Should().Be(GraphArgumentKind.List);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .As<GraphListArgument>().Elements.Should().BeEmpty();
        }

        [Fact]
        public void ListArgumentWithSingleElementReturnsListArgument()
        {
            // Arrange
            string source = @"query { user(id: [""element""]) }";

            // Act
            GraphDocument document = new Parser().Parse(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .ArgumentKind.Should().Be(GraphArgumentKind.List);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .As<GraphListArgument>().Elements[0]
                .As<GraphTypeArgument>().Value
                .As<GraphStringValue>().Value.Should().Be("element");
        }

        [Fact]
        public void ListArgumentWithMultipleElementReturnsListArgument()
        {
            // Arrange
            string source = @"query { user(id: [""element1"" ""element2"" ""element3""]) }";

            // Act
            GraphDocument document = new Parser().Parse(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .ArgumentKind.Should().Be(GraphArgumentKind.List);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .As<GraphListArgument>().Elements[0]
                .As<GraphTypeArgument>().Value
                .As<GraphStringValue>().Value.Should().Be("element1");

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .As<GraphListArgument>().Elements[1]
                .As<GraphTypeArgument>().Value
                .As<GraphStringValue>().Value.Should().Be("element2");

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .As<GraphListArgument>().Elements[2]
                .As<GraphTypeArgument>().Value
                .As<GraphStringValue>().Value.Should().Be("element3");
        }

        [Fact]
        public void NestedListArgumentReturnsNestedListArgument()
        {
            // Arrange
            string source = @"query { user(id: [[""element1""]]) }";

            // Act
            GraphDocument document = new Parser().Parse(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .ArgumentKind.Should().Be(GraphArgumentKind.List);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .As<GraphListArgument>().Elements[0]
                .As<GraphListArgument>().Elements[0]
                .As<GraphTypeArgument>().Value
                .As<GraphStringValue>().Value.Should().Be("element1");
        }

        [Fact]
        public void ListArgumentAndTypeArgumentReturnsListArgumentAndTypeArgument()
        {
            // Arrange
            string source = @"query { user(id: [""element1""] name: ""username"") }";

            // Act
            GraphDocument document = new Parser().Parse(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .ArgumentKind.Should().Be(GraphArgumentKind.List);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .As<GraphListArgument>().Elements[0]
                .As<GraphTypeArgument>().Value
                .As<GraphListValue>().Values[0]
                .As<GraphStringValue>().Value.Should().Be("element1");
            
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[1]
                .ArgumentKind.Should().Be(GraphArgumentKind.Type);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[1]
                .As<GraphTypeArgument>().Value
                .As<GraphStringValue>().Value.Should().Be("username");
        }

        [Fact]
        public void ObjectArgumentWithNoElementReturnsObjectArgument()
        {
            // Arrange
            string source = @"query { user(id: {}) }";

            // Act
            GraphDocument document = new Parser().Parse(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .ArgumentKind.Should().Be(GraphArgumentKind.Object);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .As<GraphObjectArgument>().Elements.Should().BeEmpty();
        }

        [Fact]
        public void ObjectArgumentWithSingleElementReturnsObjectArgument()
        {
            // Arrange
            string source = @"query { user(id: { id: ""1"" }) }";

            // Act
            GraphDocument document = new Parser().Parse(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .ArgumentKind.Should().Be(GraphArgumentKind.Object);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .As<GraphObjectArgument>().Elements.Should().NotBeEmpty();

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .As<GraphObjectArgument>().Elements[0]
                .As<GraphTypeArgument>().Name?.Value.Should().Be("id");

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .As<GraphObjectArgument>().Elements[0]
                .As<GraphTypeArgument>().Value
                .As<GraphStringValue>().Value.Should().Be("1");
        }

        [Fact]
        public void NestedObjectArgumentReturnsObjectArgument()
        {
            // Arrange
            string source = @"query { user(selection: { id: { prefix: ""admin-"" } }) }";

            // Act
            GraphDocument document = new Parser().Parse(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .ArgumentKind.Should().Be(GraphArgumentKind.Object);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .As<GraphObjectArgument>().Elements[0]
                .As<GraphObjectArgument>().Elements.Should().NotBeEmpty();

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .As<GraphObjectArgument>().Elements[0]
                .As<GraphObjectArgument>().Elements[0]
                .As<GraphTypeArgument>().Name?.Value.Should().Be("prefix");

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0]
                .As<GraphObjectArgument>().Elements[0]
                .As<GraphObjectArgument>().Elements[0]
                .As<GraphTypeArgument>().Value
                .As<GraphStringValue>().Value.Should().Be("admin-");
        }
    }
}
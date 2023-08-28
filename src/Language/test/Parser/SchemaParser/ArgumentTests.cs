namespace Chart.Language.Tests
{
    public class ArgumentTests
    {
        [Fact]
        public void EmptyArgumentListReturnsEmptyArguments()
        {
            // Arrange
            string source = @"query { user() }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments.Should().NotBeNull();

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments.Should().BeEmpty();
        }

        [Fact]
        public void SingleIntegerArgumentReturnsIntArgument()
        {
            // Arrange
            string source = @"query { user(id: 1) }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Name.Value
                .Should().Be("id");

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value
                .As<GraphIntValue>().Value.Should().Be(1);
        }

        [Fact]
        public void ListArgumentWithNoElementReturnsListArgument()
        {
            // Arrange
            string source = @"query { user(id: []) }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value.ValueKind
                .Should().Be(GraphValueKind.List);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value
                .As<GraphListValue>().Value.Should().BeEmpty();
        }

        [Fact]
        public void ListArgumentWithSingleElementReturnsListArgument()
        {
            // Arrange
            string source = @"query { user(id: [""element""]) }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value.ValueKind
                .Should().Be(GraphValueKind.List);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value
                .As<GraphListValue>().Value[0]
                .As<GraphStringValue>().Value.Should().Be("element");
        }

        [Fact]
        public void ListArgumentWithMultipleElementReturnsListArgument()
        {
            // Arrange
            string source = @"query { user(id: [""element1"" ""element2"" ""element3""]) }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value.ValueKind
                .Should().Be(GraphValueKind.List);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value
                .As<GraphListValue>().Value[0].ValueKind
                .Should().Be(GraphValueKind.String);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value
                .As<GraphListValue>().Value[0]
                .As<GraphStringValue>().Value.Should().Be("element1");

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value
                .As<GraphListValue>().Value[1]
                .As<GraphStringValue>().Value.Should().Be("element2");

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value
                .As<GraphListValue>().Value[2]
                .As<GraphStringValue>().Value.Should().Be("element3");
        }

        [Fact]
        public void NestedListArgumentReturnsNestedListArgument()
        {
            // Arrange
            string source = @"query { user(id: [[""element1""]]) }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value.ValueKind
                .Should().Be(GraphValueKind.List);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value
                .As<GraphListValue>().Value[0].ValueKind
                .Should().Be(GraphValueKind.List);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value
                .As<GraphListValue>().Value[0]
                .As<GraphListValue>().Value[0]
                .As<GraphStringValue>().Value.Should().Be("element1");
        }

        [Fact]
        public void ListArgumentAndStringArgumentReturnsListArgumentAndStringArgument()
        {
            // Arrange
            string source = @"query { user(id: [""element1""] name: ""username"") }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value.ValueKind
                .Should().Be(GraphValueKind.List);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value
                .As<GraphListValue>().Value[0]
                .As<GraphStringValue>().Value.Should().Be("element1");

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[1].Value.ValueKind
                .Should().Be(GraphValueKind.String);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[1].Value
                .As<GraphStringValue>().Value.Should().Be("username");
        }

        [Fact]
        public void ObjectArgumentWithNoElementReturnsObjectArgument()
        {
            // Arrange
            string source = @"query { user(id: {}) }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value.ValueKind
                .Should().Be(GraphValueKind.Object);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value
                .As<GraphObjectValue>().Value.Should().BeEmpty();
        }

        [Fact]
        public void ObjectArgumentWithSingleElementReturnsObjectArgument()
        {
            // Arrange
            string source = @"query { user(id: { id: ""1"" }) }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value.ValueKind
                .Should().Be(GraphValueKind.Object);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value
                .As<GraphObjectValue>().Value.Should().NotBeEmpty();

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value
                .As<GraphObjectValue>().Value.Should().ContainKey(new GraphName("id"));

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value
                .As<GraphObjectValue>().Value[new GraphName("id")]
                .As<GraphStringValue>().Value.Should().Be("1");
        }

        [Fact]
        public void NestedObjectArgumentReturnsObjectArgument()
        {
            // Arrange
            string source = @"query { user(selection: { id: { prefix: ""admin-"" } }) }";

            // Act
            GraphDocument document = new SchemaParser().ParseQuery(source);

            // Assert
            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value.ValueKind
                .Should().Be(GraphValueKind.Object);

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Name.Value
                .Should().Be("selection");

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value
                .As<GraphObjectValue>().Value[new GraphName("id")]
                .As<GraphObjectValue>().Value.Should().ContainKey(new GraphName("prefix"));

            document.Definitions[0]
                .As<GraphOperationDefinition>().Selections.Selections[0]
                .As<GraphFieldSelection>().Arguments?.Arguments[0].Value
                .As<GraphObjectValue>().Value[new GraphName("id")]
                .As<GraphObjectValue>().Value[new GraphName("prefix")]
                .As<GraphStringValue>().Value.Should().Be("admin-");
        }

        [Fact]
        public void ArgumentWithDefaultValueThrowsDefaultValuesNotAllowedException()
        {
            // Arrange
            string source = @"query { user(id: 1 = 1) }";

            // Act
            Action act = () => new SchemaParser().ParseQuery(source);

            // Assert
            act.Should().Throw<DefaultValuesNotAllowedException>();
        }
    }
}
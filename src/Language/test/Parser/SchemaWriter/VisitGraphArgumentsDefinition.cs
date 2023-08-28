namespace Chart.Language.SyntaxTree.Tests.SchemaWriterTests
{
    public class VisitGraphArgumentsDefinitionTests
    {
        [Fact]
        public void Visit_PrintsArguments_GivenEmptyArgumentsDefinition()
        {
            // Arrange
            GraphArgumentsDefinition argumentsDefinition = new GraphArgumentsDefinition();

            // Act

            // Assert
            new SchemaWriter()
                .Visit(argumentsDefinition)
                .ToString()
                .Should().BeEmpty();
        }

        [Fact]
        public void Visit_PrintsArguments_GivenSingleArgumentDefinition()
        {
            // Arrange
            GraphArgumentsDefinition argumentsDefinition = new GraphArgumentsDefinition();
            argumentsDefinition.Arguments.Add(new GraphArgumentDefinition
            {
                Name = new GraphName("filter"),
                Type = new GraphListType(new GraphNamedType("String"))
            });

            // Act

            // Assert
            new SchemaWriter()
                .Visit(argumentsDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsArguments_GivenSingleArgumentDefinitionWithDescription()
        {
            // Arrange
            GraphArgumentsDefinition argumentsDefinition = new GraphArgumentsDefinition();
            argumentsDefinition.Arguments.Add(new GraphArgumentDefinition
            {
                Name = new GraphName("filter"),
                Type = new GraphListType(new GraphNamedType("String")),
                Description = new GraphDescription("An optional filter of the queries.")
            });

            // Act

            // Assert
            new SchemaWriter()
                .Visit(argumentsDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsArguments_GivenSingleArgumentDefinitionWithDirectives()
        {
            // Arrange
            GraphArgumentsDefinition argumentsDefinition = new GraphArgumentsDefinition();
            argumentsDefinition.Arguments.Add(new GraphArgumentDefinition
            {
                Name = new GraphName("filter"),
                Type = new GraphListType(new GraphNamedType("String")),
                Directives = new GraphDirectives()
                {
                    Directives = new List<GraphDirective>()
                    {
                        new GraphDirective
                        {
                            Name = new GraphName("include")
                        },
                        new GraphDirective
                        {
                            Name = new GraphName("skip")
                        }
                    }
                }
            });

            // Act

            // Assert
            new SchemaWriter()
                .Visit(argumentsDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsArguments_GivenSingleArgumentDefinitionWithDefaultValue()
        {
            // Arrange
            GraphArgumentsDefinition argumentsDefinition = new GraphArgumentsDefinition();
            argumentsDefinition.Arguments.Add(new GraphArgumentDefinition
            {
                Name = new GraphName("filter"),
                Type = new GraphListType(new GraphNamedType("String")),
                DefaultValue = new GraphListValue(new List<IGraphValue> { new GraphStringValue("Max") })
            });

            // Act

            // Assert
            new SchemaWriter()
                .Visit(argumentsDefinition)
                .ToString()
                .MatchSnapshot();
        }
    }
}
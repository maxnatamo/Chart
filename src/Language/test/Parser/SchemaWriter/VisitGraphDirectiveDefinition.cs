namespace Chart.Language.SyntaxTree.Tests.SchemaWriterTests
{
    public class VisitGraphDirectiveDefinitionTests
    {
        [Fact]
        public void Visit_PrintsDirective_GivenSingleDirectiveLocation()
        {
            // Arrange
            GraphDirectiveDefinition directiveDefinition = new GraphDirectiveDefinition();
            directiveDefinition.Name = new GraphName("directive");
            directiveDefinition.Locations.Locations = GraphDirectiveLocation.QUERY;

            // Act

            // Assert
            new SchemaWriter()
                .Visit((GraphDefinition) directiveDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsDirective_GivenDirectiveWithDescription()
        {
            // Arrange
            GraphDirectiveDefinition directiveDefinition = new GraphDirectiveDefinition();
            directiveDefinition.Name = new GraphName("directive");
            directiveDefinition.Description = new GraphDescription("A cool description.");
            directiveDefinition.Locations.Locations = GraphDirectiveLocation.QUERY;

            // Act

            // Assert
            new SchemaWriter()
                .Visit((GraphDefinition) directiveDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsDirective_GivenMultipleDirectiveLocation()
        {
            // Arrange
            GraphDirectiveDefinition directiveDefinition = new GraphDirectiveDefinition();
            directiveDefinition.Name = new GraphName("directive");
            directiveDefinition.Locations.Locations =
                GraphDirectiveLocation.QUERY |
                GraphDirectiveLocation.MUTATION |
                GraphDirectiveLocation.SUBSCRIPTION;

            // Act

            // Assert
            new SchemaWriter()
                .Visit((GraphDefinition) directiveDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsDirective_GivenRepeatableDirective()
        {
            // Arrange
            GraphDirectiveDefinition directiveDefinition = new GraphDirectiveDefinition();
            directiveDefinition.Name = new GraphName("directive");
            directiveDefinition.Locations.Locations = GraphDirectiveLocation.QUERY;
            directiveDefinition.Repeatable = true;

            // Act

            // Assert
            new SchemaWriter()
                .Visit((GraphDefinition) directiveDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsDirective_GivenDirectiveWithArgument()
        {
            // Arrange
            GraphDirectiveDefinition directiveDefinition = new GraphDirectiveDefinition();
            directiveDefinition.Name = new GraphName("directive");
            directiveDefinition.Locations.Locations = GraphDirectiveLocation.QUERY;
            directiveDefinition.Arguments = new GraphArgumentsDefinition()
            {
                Arguments = new List<GraphArgumentDefinition>
                {
                    new GraphArgumentDefinition()
                    {
                        Name = new GraphName("argument1"),
                        Type = new GraphNamedType("String")
                    }
                }
            };

            // Act

            // Assert
            new SchemaWriter()
                .Visit((GraphDefinition) directiveDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsDirective_GivenDirectiveWithMultipleArguments()
        {
            // Arrange
            GraphDirectiveDefinition directiveDefinition = new GraphDirectiveDefinition();
            directiveDefinition.Name = new GraphName("directive");
            directiveDefinition.Locations.Locations = GraphDirectiveLocation.QUERY;
            directiveDefinition.Arguments = new GraphArgumentsDefinition()
            {
                Arguments = new List<GraphArgumentDefinition>
                {
                    new GraphArgumentDefinition()
                    {
                        Name = new GraphName("argument1"),
                        Type = new GraphNamedType("String")
                    },
                    new GraphArgumentDefinition()
                    {
                        Name = new GraphName("argument2"),
                        Type = new GraphNamedType("Int")
                    }
                }
            };

            // Act

            // Assert
            new SchemaWriter()
                .Visit((GraphDefinition) directiveDefinition)
                .ToString()
                .MatchSnapshot();
        }
    }
}
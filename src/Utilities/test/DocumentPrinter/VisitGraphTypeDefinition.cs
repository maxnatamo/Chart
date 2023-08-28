namespace Chart.Utilities.Tests.DocumentPrinterTests
{
    public class VisitGraphTypeDefinitionTests
    {
        [Fact]
        public void Visit_PrintsTypeDefinition_GivenEmptyScalarType()
        {
            // Arrange
            GraphScalarType typeDefinition = new GraphScalarType();
            typeDefinition.Name = new GraphName("DateTime");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) typeDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsTypeDefinition_GivenEmptyScalarTypeWithDescription()
        {
            // Arrange
            GraphScalarType typeDefinition = new GraphScalarType();
            typeDefinition.Name = new GraphName("DateTime");
            typeDefinition.Description = new GraphDescription("A data type for dates with times");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) typeDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsTypeDefinition_GivenEmptyScalarTypeWithDirectives()
        {
            // Arrange
            GraphScalarType typeDefinition = new GraphScalarType();
            typeDefinition.Name = new GraphName("DateTime");
            typeDefinition.Directives = new GraphDirectives()
            {
                Directives = new List<GraphDirective>
                {
                    new GraphDirective
                    {
                        Name = new GraphName("deprecated")
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) typeDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsTypeDefinition_GivenEmptyInputType()
        {
            // Arrange
            GraphInputDefinition typeDefinition = new GraphInputDefinition();
            typeDefinition.Name = new GraphName("Person");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) typeDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsTypeDefinition_GivenEmptyInputTypeWithArguments()
        {
            // Arrange
            GraphInputDefinition typeDefinition = new GraphInputDefinition();
            typeDefinition.Name = new GraphName("Person");
            typeDefinition.Fields = new GraphInputFieldsDefinition()
            {
                Arguments = new List<GraphArgumentDefinition>
                {
                    new GraphArgumentDefinition()
                    {
                        Name = new GraphName("name"),
                        Type = new GraphNamedType("String")
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) typeDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsTypeDefinition_GivenEmptyObjectType()
        {
            // Arrange
            GraphObjectType typeDefinition = new GraphObjectType();
            typeDefinition.Name = new GraphName("Person");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) typeDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsTypeDefinition_GivenEmptyObjectTypeWithInterfaces()
        {
            // Arrange
            GraphObjectType typeDefinition = new GraphObjectType();
            typeDefinition.Name = new GraphName("Person");
            typeDefinition.Interface = new GraphInterfaces()
            {
                Implements = new List<GraphNamedType>
                {
                    new GraphNamedType("Creature"),
                    new GraphNamedType("OxygenBased")
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) typeDefinition)
                .ToString()
                .MatchSnapshot();
        }
    }
}
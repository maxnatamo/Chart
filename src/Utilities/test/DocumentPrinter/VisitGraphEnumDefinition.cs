namespace Chart.Utilities.Tests.DocumentPrinterTests
{
    public class VisitGraphEnumDefinitionTests
    {
        [Fact]
        public void Visit_PrintsEnum_GivenEmptyEnum()
        {
            // Arrange
            GraphEnumDefinition enumDefinition = new GraphEnumDefinition();
            enumDefinition.Name = new GraphName("enumeration");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) enumDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsEnum_GivenEmptyEnumWithDirectives()
        {
            // Arrange
            GraphEnumDefinition enumDefinition = new GraphEnumDefinition();
            enumDefinition.Name = new GraphName("Direction");
            enumDefinition.Directives = new GraphDirectives()
            {
                Directives = new List<GraphDirective>()
                {
                    new GraphDirective
                    {
                        Name = new GraphName("include")
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) enumDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsEnum_GivenEmptyEnumWithDescription()
        {
            // Arrange
            GraphEnumDefinition enumDefinition = new GraphEnumDefinition();
            enumDefinition.Name = new GraphName("enumeration");
            enumDefinition.Description = new GraphDescription("A custom description.");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) enumDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsEnum_GivenEnumWithSingleValue()
        {
            // Arrange
            GraphEnumDefinition enumDefinition = new GraphEnumDefinition();
            enumDefinition.Name = new GraphName("enumeration");
            enumDefinition.Values = new GraphEnumDefinitionValues()
            {
                Values = new List<GraphEnumDefinitionValue>
                {
                    new GraphEnumDefinitionValue
                    {
                        Name = new GraphName("ENUM_VALUE_ONE")
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) enumDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsEnum_GivenEnumWithSingleValueWithDescription()
        {
            // Arrange
            GraphEnumDefinition enumDefinition = new GraphEnumDefinition();
            enumDefinition.Name = new GraphName("enumeration");
            enumDefinition.Values = new GraphEnumDefinitionValues()
            {
                Values = new List<GraphEnumDefinitionValue>
                {
                    new GraphEnumDefinitionValue
                    {
                        Name = new GraphName("ENUM_VALUE_ONE"),
                        Description = new GraphDescription("The most bare-bone enum ever.")
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) enumDefinition)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsEnum_GivenEnumWithSingleValueWithDirective()
        {
            // Arrange
            GraphEnumDefinition enumDefinition = new GraphEnumDefinition();
            enumDefinition.Name = new GraphName("enumeration");
            enumDefinition.Values = new GraphEnumDefinitionValues()
            {
                Values = new List<GraphEnumDefinitionValue>
                {
                    new GraphEnumDefinitionValue
                    {
                        Name = new GraphName("ENUM_VALUE_ONE"),
                        Directives = new GraphDirectives()
                        {
                            Directives = new List<GraphDirective>
                            {
                                new GraphDirective
                                {
                                    Name = new GraphName("obselete"),
                                    Arguments = new GraphArguments()
                                    {
                                        Arguments = new List<GraphArgument>
                                        {
                                            new GraphArgument
                                            {
                                                Name = new GraphName("by"),
                                                Value = new GraphStringValue("v1.2")
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphDefinition) enumDefinition)
                .ToString()
                .MatchSnapshot();
        }
    }
}
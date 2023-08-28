namespace Chart.Utilities.Tests.DocumentPrinterTests
{
    public class VisitGraphFieldSelectionTests
    {
        [Fact]
        public void Visit_PrintsLeafField_GivenEmptySelection()
        {
            // Arrange
            GraphFieldSelection fieldSelection = new GraphFieldSelection();
            fieldSelection.Name = new GraphName("name");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphSelection) fieldSelection)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsAliasedLeafField_GivenEmptySelectionWithAlias()
        {
            // Arrange
            GraphFieldSelection fieldSelection = new GraphFieldSelection();
            fieldSelection.Name = new GraphName("name");
            fieldSelection.Alias = new GraphName("fullName");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphSelection) fieldSelection)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsField_GivenEmptySelectionWithArguments()
        {
            // Arrange
            GraphFieldSelection fieldSelection = new GraphFieldSelection();
            fieldSelection.Name = new GraphName("name");
            fieldSelection.Arguments = new GraphArguments()
            {
                Arguments = new List<GraphArgument>()
                {
                    new GraphArgument
                    {
                        Name = new GraphName("where"),
                        Value = new GraphStringValue("age >= 18")
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphSelection) fieldSelection)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsField_GivenEmptySelectionWithDirectives()
        {
            // Arrange
            GraphFieldSelection fieldSelection = new GraphFieldSelection();
            fieldSelection.Name = new GraphName("name");
            fieldSelection.Directives = new GraphDirectives()
            {
                Directives = new List<GraphDirective>
                {
                    new GraphDirective
                    {
                        Name = new GraphName("include"),
                        Arguments = new GraphArguments()
                        {
                            Arguments = new List<GraphArgument>
                            {
                                new GraphArgument
                                {
                                    Name = new GraphName("if"),
                                    Value = new GraphVariableValue("nameIncluded")
                                }
                            }
                        }
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphSelection) fieldSelection)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsField_GivenSelectionWithSelections()
        {
            // Arrange
            GraphFieldSelection fieldSelection = new GraphFieldSelection();
            fieldSelection.Name = new GraphName("person");
            fieldSelection.SelectionSet = new GraphSelectionSet
            {
                Selections = new List<GraphSelection>
                {
                    new GraphFieldSelection()
                    {
                        Name = new GraphName("name")
                    },
                    new GraphFieldSelection()
                    {
                        Name = new GraphName("age")
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit((GraphSelection) fieldSelection)
                .ToString()
                .MatchSnapshot();
        }
    }
}
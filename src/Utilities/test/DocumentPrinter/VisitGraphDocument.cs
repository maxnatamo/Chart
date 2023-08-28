namespace Chart.Utilities.Tests.DocumentPrinterTests
{
    public class VisitGraphDocumentTests
    {
        [Fact]
        public void Visit_PrintsDocument_GivenEmptyDocument()
        {
            // Arrange
            GraphDocument document = new GraphDocument();

            // Act

            // Assert
            new DocumentPrinter()
                .Visit(document)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsDocument_GivenSingleOperationDocument()
        {
            // Arrange
            GraphDocument document = new GraphDocument()
            {
                Definitions = new List<GraphDefinition>
                {
                    new GraphQueryOperation()
                    {
                        Selections = new GraphSelectionSet()
                        {
                            Selections = new List<GraphSelection>
                            {
                                new GraphFieldSelection()
                                {
                                    Name = new GraphName("admin")
                                }
                            }
                        }
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit(document)
                .ToString()
                .MatchSnapshot();
        }
    }
}
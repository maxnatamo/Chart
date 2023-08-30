namespace Chart.Utilities.Tests.DocumentPrinterTests
{
    public class VisitGraphDirectiveTests
    {
        [Fact]
        public void PrintsNamedDirective()
        {
            // Arrange
            GraphDirective directives = new GraphDirective();
            directives.Name = new GraphName("directive");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit(directives)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void PrintsDirectiveWithArguments()
        {
            // Arrange
            GraphDirective directive = new GraphDirective();
            directive.Name = new GraphName("directive");
            directive.Arguments = new()
            {
                Arguments = new()
                {
                    new GraphArgument()
                    {
                        Name = new("arg1"),
                        Value = new GraphIntValue(1)
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit(directive)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void SkipsPrintPrintForEmptyDirectives()
        {
            // Arrange
            GraphDirectives directives = new();

            // Act

            // Assert
            new DocumentPrinter()
                .Visit(directives)
                .ToString()
                .Should().BeEmpty();
        }
    }
}
namespace Chart.Utilities.Tests.DocumentPrinterTests
{
    public class VisitGraphFieldsTests
    {
        [Fact]
        public void PrintsNamedField()
        {
            // Arrange
            GraphField field = new GraphField();
            field.Name = new GraphName("field");
            field.Type = new GraphNamedType("Int");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit(field)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void PrintsFieldWithDescription()
        {
            // Arrange
            GraphField field = new GraphField();
            field.Name = new GraphName("field");
            field.Type = new GraphNamedType("Int");
            field.Description = new GraphDescription("This is a field");

            // Act

            // Assert
            new DocumentPrinter()
                .Visit(field)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void PrintsFieldWithDirectives()
        {
            // Arrange
            GraphField field = new GraphField();
            field.Name = new GraphName("field");
            field.Type = new GraphNamedType("Int");
            field.Directives = new()
            {
                Directives = new()
                {
                    new GraphDirective()
                    {
                        Name = new GraphName("directive")
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit(field)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void PrintsFieldWithArguments()
        {
            // Arrange
            GraphField field = new GraphField();
            field.Name = new GraphName("field");
            field.Type = new GraphNamedType("Int");
            field.Arguments = new()
            {
                Arguments = new()
                {
                    new GraphArgumentDefinition()
                    {
                        Name = new GraphName("directive"),
                        Type = new GraphNamedType("arg")
                    }
                }
            };

            // Act

            // Assert
            new DocumentPrinter()
                .Visit(field)
                .ToString()
                .MatchSnapshot();
        }
    }
}
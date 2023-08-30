namespace Chart.Utilities.Tests.DocumentPrinterTests
{
    public class VisitTests
    {
        private class CustomDefinition : GraphDefinition
        {
            public override GraphDefinitionKind DefinitionKind => (GraphDefinitionKind) 400;

            public override bool Executable => false;

            public override string ToString()
                => string.Empty;
        }

        [Fact]
        public void ThrowsWithInvalidDefinitionType()
        {
            // Arrange
            CustomDefinition customDefinition = new();
            DocumentPrinter printer = new();

            // Act
            Action act = () => printer.Visit(customDefinition);

            // Assert
            act.Should().Throw<Exception>();
        }
    }
}
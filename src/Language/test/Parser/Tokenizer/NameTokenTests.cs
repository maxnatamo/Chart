namespace Chart.Language.Parsers.Tests
{
    public class NameTokenTests
    {
        [Fact]
        public void NameTokenWithWhitespaceReturnsOnlyName()
        {
            // Arrange
            string source = "     name \n\n";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            Token token = tokenizer.GetNextToken();

            // Assert
            token.Start.Should().Be(5);
            token.End.Should().Be(9);
            token.Type.Should().Be(TokenType.NAME);
            token.Value.Should().Be("name");
        }
    }
}
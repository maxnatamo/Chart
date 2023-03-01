namespace Chart.Core.Parsers.Tests
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
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens.Count.Should().Be(2);
            tokens[0].Start.Should().Be(5);
            tokens[0].End.Should().Be(9);
            tokens[0].Type.Should().Be(TokenType.NAME);
            tokens[0].Value.Should().Be("name");
        }

        [Fact]
        public void NameTokenWithCommentsReturnsNameAndComments()
        {
            // Arrange
            string source = "     name\n#comment";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens.Count.Should().Be(3);

            tokens[0].Start.Should().Be(5);
            tokens[0].End.Should().Be(9);
            tokens[0].Type.Should().Be(TokenType.NAME);
            tokens[0].Value.Should().Be("name");

            tokens[1].Start.Should().Be(10);
            tokens[1].End.Should().Be(18);
            tokens[1].Type.Should().Be(TokenType.COMMENT);
            tokens[1].Value.Should().Be("comment");
        }
    }
}
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

        [Fact]
        public void NameTokenWithCommentsReturnsNameAndComments()
        {
            // Arrange
            string source = "     name\n#comment";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = new List<Token>
            {
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken(),
            };

            // Assert
            tokens.Should().SatisfyRespectively(
                first =>
                {
                    first.Start.Should().Be(5);
                    first.End.Should().Be(9);
                    first.Type.Should().Be(TokenType.NAME);
                    first.Value.Should().Be("name");
                },
                second =>
                {
                    second.Start.Should().Be(10);
                    second.End.Should().Be(18);
                    second.Type.Should().Be(TokenType.COMMENT);
                    second.Value.Should().Be("comment");
                },
                third => third.Type.Should().Be(TokenType.EOF)
            );
        }
    }
}
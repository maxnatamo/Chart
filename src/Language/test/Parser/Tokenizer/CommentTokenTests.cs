namespace Chart.Language.Parsers.Tests
{
    public class CommentTokenTests
    {
        [Fact]
        public void MultilineCommentsReturnMultipleTokens()
        {
            // Arrange
            string source = "#comment1\n#comment2\n#comment3";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = new List<Token>
            {
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken(),
            };

            // Assert
            tokens.Should().SatisfyRespectively(
                first =>
                {
                    first.Start.Should().Be(0);
                    first.End.Should().Be(9);
                    first.Type.Should().Be(TokenType.COMMENT);
                    first.Value.Should().Be("comment1");
                },
                second =>
                {
                    second.Start.Should().Be(10);
                    second.End.Should().Be(19);
                    second.Type.Should().Be(TokenType.COMMENT);
                    second.Value.Should().Be("comment2");
                },
                third =>
                {
                    third.Start.Should().Be(20);
                    third.End.Should().Be(29);
                    third.Type.Should().Be(TokenType.COMMENT);
                    third.Value.Should().Be("comment3");
                },
                fourth => fourth.Type.Should().Be(TokenType.EOF)
            );
        }
        [Fact]
        public void CommentsWithSpacesReturnTrimmedContent()
        {
            // Arrange
            string source = "#  Super Comment  ";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            Token token = tokenizer.GetNextToken();

            // Assert
            token.Start.Should().Be(0);
            token.End.Should().Be(18);
            token.Type.Should().Be(TokenType.COMMENT);
            token.Value.Should().Be("Super Comment");
        }
    }
}
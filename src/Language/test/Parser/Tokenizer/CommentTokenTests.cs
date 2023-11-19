namespace Chart.Language.Parsers.Tests
{
    public class CommentTokenTests
    {
        [Fact]
        public void Comment_ReturnsCommentToken()
        {
            // Arrange
            string source = "# name";
            Tokenizer tokenizer = new(source);

            // Act
            Token token = tokenizer.ReadToken();

            // Assert
            token.Type.Should().Be(TokenType.COMMENT);
            token.Value.Should().NotStartWith("#");
            token.Value.Should().Be("name");
        }

        [Theory]
        [InlineData('\r')]
        [InlineData('\n')]
        public void CommentWithNewline_ReturnsCommentToken(char newline)
        {
            // Arrange
            string source = $"# name{newline}not comment";
            Tokenizer tokenizer = new(source);

            // Act
            Token token = tokenizer.ReadToken();

            // Assert
            token.Type.Should().Be(TokenType.COMMENT);
            token.Value.Should().Be("name");
        }
    }
}
namespace Chart.Core.Parsers.Tests
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
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens.Count.Should().Be(4);

            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(9);
            tokens[0].Type.Should().Be(TokenType.COMMENT);
            tokens[0].Value.Should().Be("comment1");

            tokens[1].Start.Should().Be(10);
            tokens[1].End.Should().Be(19);
            tokens[1].Type.Should().Be(TokenType.COMMENT);
            tokens[1].Value.Should().Be("comment2");

            tokens[2].Start.Should().Be(20);
            tokens[2].End.Should().Be(29);
            tokens[2].Type.Should().Be(TokenType.COMMENT);
            tokens[2].Value.Should().Be("comment3");
        }
        [Fact]
        public void CommentsWithSpacesReturnTrimmedContent()
        {
            // Arrange
            string source = "#  Super Comment  ";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens.Count.Should().Be(2);

            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(18);
            tokens[0].Type.Should().Be(TokenType.COMMENT);
            tokens[0].Value.Should().Be("Super Comment");
        }
    }
}
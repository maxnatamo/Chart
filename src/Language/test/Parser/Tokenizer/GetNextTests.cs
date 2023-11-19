namespace Chart.Language.Parsers.Tests
{
    public class GetNextTests
    {
        [Fact]
        public void GetNext_SkipsComment()
        {
            // Arrange
            string source = "str\n#comment\n4\nfalse";
            Tokenizer tokenizer = new(source);

            // Act
            Token[] tokens = new Token[]
            {
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken()
            };

            // Assert
            tokens.Should().SatisfyRespectively(
                v => v.Type.Should().Be(TokenType.NAME),
                v => v.Type.Should().Be(TokenType.INT),
                v => v.Type.Should().Be(TokenType.BOOLEAN));
        }

        [Fact]
        public void GetNext_SkipsMultipleComments()
        {
            // Arrange
            string source = "str\n#comment\n#comment\n#comment\n4\nfalse";
            Tokenizer tokenizer = new(source);

            // Act
            Token[] tokens = new Token[]
            {
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken()
            };

            // Assert
            tokens.Should().SatisfyRespectively(
                v => v.Type.Should().Be(TokenType.NAME),
                v => v.Type.Should().Be(TokenType.INT),
                v => v.Type.Should().Be(TokenType.BOOLEAN));
        }
    }
}
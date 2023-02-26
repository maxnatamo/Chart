namespace Chart.Core.Parser.Tests
{
    public class NullTokenTests
    {
        [Fact]
        public void LowercaseNullReturnsNullToken()
        {
            // Arrange
            string source = "null";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens.Count.Should().Be(2);

            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(4);
            tokens[0].Type.Should().Be(TokenType.NULL);
        }

        [Fact]
        public void UppercaseNullReturnsNameToken()
        {
            // Arrange
            string source = "NULL";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens.Count.Should().Be(2);

            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(4);
            tokens[0].Type.Should().Be(TokenType.NAME);
            tokens[0].Value.Should().Be("NULL");
        }

        [Fact]
        public void NullInQuotesReturnsStringToken()
        {
            // Arrange
            string source = @" ""null"" ";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens.Count.Should().Be(2);

            tokens[0].Start.Should().Be(1);
            tokens[0].End.Should().Be(7);
            tokens[0].Type.Should().Be(TokenType.STRING);
            tokens[0].Value.Should().Be("null");
        }
    }
}
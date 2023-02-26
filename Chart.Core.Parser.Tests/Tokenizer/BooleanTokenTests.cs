namespace Chart.Core.Parser.Tests
{
    public class BooleanTokenTests
    {
        [Fact]
        public void LowercaseFalseReturnsBooleanToken()
        {
            // Arrange
            string source = "false";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(5);
            tokens[0].Type.Should().Be(TokenType.BOOLEAN);
            tokens[0].Value.Should().Be("false");
        }

        [Fact]
        public void LowercaseTrueReturnsBooleanToken()
        {
            // Arrange
            string source = "true";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(4);
            tokens[0].Type.Should().Be(TokenType.BOOLEAN);
            tokens[0].Value.Should().Be("true");
        }

        [Fact]
        public void UppercaseFalseReturnsNameToken()
        {
            // Arrange
            string source = "FALSE";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(5);
            tokens[0].Type.Should().Be(TokenType.NAME);
            tokens[0].Value.Should().Be("FALSE");
        }

        [Fact]
        public void BooleanInQuotesReturnsStringToken()
        {
            // Arrange
            string source = @" ""false"" ";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens[0].Start.Should().Be(1);
            tokens[0].End.Should().Be(8);
            tokens[0].Type.Should().Be(TokenType.STRING);
            tokens[0].Value.Should().Be("false");
        }
    }
}
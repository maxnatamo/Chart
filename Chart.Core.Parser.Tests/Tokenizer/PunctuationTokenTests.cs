namespace Chart.Core.Parser.Tests
{
    public class PunctuationTokenTests
    {
        [Fact]
        public void ColonReturnsColonToken()
        {
            // Arrange
            string source = ":";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens.Count.Should().Be(2);
            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(1);
            tokens[0].Type.Should().Be(TokenType.COLON);
        }

        [Fact]
        public void LeftParenthesisReturnsLeftParenthesisToken()
        {
            // Arrange
            string source = "(";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens.Count.Should().Be(2);
            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(1);
            tokens[0].Type.Should().Be(TokenType.PARENTHESIS_LEFT);
        }

        [Fact]
        public void RightParenthesisReturnsRightParenthesisToken()
        {
            // Arrange
            string source = ")";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens.Count.Should().Be(2);
            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(1);
            tokens[0].Type.Should().Be(TokenType.PARENTHESIS_RIGHT);
        }

        [Fact]
        public void LeftBraceReturnsLeftBracesToken()
        {
            // Arrange
            string source = "{";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens.Count.Should().Be(2);
            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(1);
            tokens[0].Type.Should().Be(TokenType.BRACE_LEFT);
        }

        [Fact]
        public void RightBracesReturnsRightBracesToken()
        {
            // Arrange
            string source = "}";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens.Count.Should().Be(2);
            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(1);
            tokens[0].Type.Should().Be(TokenType.BRACE_RIGHT);
        }

        [Fact]
        public void LeftBracketReturnsLeftBracketToken()
        {
            // Arrange
            string source = "[";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens.Count.Should().Be(2);
            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(1);
            tokens[0].Type.Should().Be(TokenType.BRACKET_LEFT);
        }

        [Fact]
        public void RightBracketReturnsRightBracketToken()
        {
            // Arrange
            string source = "]";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = tokenizer.GetAllTokens();

            // Assert
            tokens.Count.Should().Be(2);
            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(1);
            tokens[0].Type.Should().Be(TokenType.BRACKET_RIGHT);
        }
    }
}
namespace Chart.Core.Parsers.Tests
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
            Token token = tokenizer.GetNextToken();

            // Assert
            token.Start.Should().Be(0);
            token.End.Should().Be(5);
            token.Type.Should().Be(TokenType.BOOLEAN);
            token.Value.Should().Be("false");
        }

        [Fact]
        public void LowercaseTrueReturnsBooleanToken()
        {
            // Arrange
            string source = "true";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            Token token = tokenizer.GetNextToken();

            // Assert
            token.Start.Should().Be(0);
            token.End.Should().Be(4);
            token.Type.Should().Be(TokenType.BOOLEAN);
            token.Value.Should().Be("true");
        }

        [Fact]
        public void UppercaseFalseReturnsNameToken()
        {
            // Arrange
            string source = "FALSE";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            Token token = tokenizer.GetNextToken();

            // Assert
            token.Start.Should().Be(0);
            token.End.Should().Be(5);
            token.Type.Should().Be(TokenType.NAME);
            token.Value.Should().Be("FALSE");
        }

        [Fact]
        public void BooleanInQuotesReturnsStringToken()
        {
            // Arrange
            string source = @" ""false"" ";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            Token token = tokenizer.GetNextToken();

            // Assert
            token.Start.Should().Be(1);
            token.End.Should().Be(8);
            token.Type.Should().Be(TokenType.STRING);
            token.Value.Should().Be("false");
        }
    }
}
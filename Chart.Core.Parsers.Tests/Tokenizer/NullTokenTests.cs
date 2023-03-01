namespace Chart.Core.Parsers.Tests
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
            Token token = tokenizer.GetNextToken();

            // Assert
            token.Start.Should().Be(0);
            token.End.Should().Be(4);
            token.Type.Should().Be(TokenType.NULL);
        }

        [Fact]
        public void UppercaseNullReturnsNameToken()
        {
            // Arrange
            string source = "NULL";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            Token token = tokenizer.GetNextToken();

            // Assert
            token.Start.Should().Be(0);
            token.End.Should().Be(4);
            token.Type.Should().Be(TokenType.NAME);
            token.Value.Should().Be("NULL");
        }

        [Fact]
        public void NullInQuotesReturnsStringToken()
        {
            // Arrange
            string source = @" ""null"" ";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            Token token = tokenizer.GetNextToken();

            // Assert
            token.Start.Should().Be(1);
            token.End.Should().Be(7);
            token.Type.Should().Be(TokenType.STRING);
            token.Value.Should().Be("null");
        }
    }
}
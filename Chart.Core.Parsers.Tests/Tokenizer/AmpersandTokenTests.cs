namespace Chart.Core.Parsers.Tests
{
    public class AmpersandTokenTests
    {
        [Fact]
        public void AmpersandOperatorReturnsPipeToken()
        {
            // Arrange
            string source = "&";

            // Act
            Token token = new Tokenizer(source).GetNextToken();

            // Assert
            token.Start.Should().Be(0);
            token.End.Should().Be(1);
            token.Type.Should().Be(TokenType.AMPERSAND);
        }
    }
}
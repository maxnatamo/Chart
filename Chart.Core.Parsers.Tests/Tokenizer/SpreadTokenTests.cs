namespace Chart.Core.Parsers.Tests
{
    public class SpreadTokenTests
    {
        [Fact]
        public void SpreadOperatorReturnsSpreadToken()
        {
            // Arrange
            string source = "...";

            // Act
            Token token = new Tokenizer(source).GetNextToken();

            // Assert
            token.Start.Should().Be(0);
            token.End.Should().Be(3);
            token.Type.Should().Be(TokenType.SPREAD);
        }
    }
}
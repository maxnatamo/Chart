using System.Text;

namespace Chart.Core.Parsers.Tests
{
    public class ReadTokenTests
    {
        [Fact]
        public void ReadTokenGivenControlCharacterThrowsInvalidDataException()
        {
            // Arrange
            string source = new ASCIIEncoding().GetString(new byte[] { 0x10 });

            // Act
            Action act = () => new Tokenizer(source).GetNextToken();

            // Assert
            act.Should().Throw<InvalidDataException>();
        }

        [Fact]
        public void ReadTokenGivenSlashReturnsUnknownToken()
        {
            // Arrange
            string source = "/";

            // Act
            Token token = new Tokenizer(source).GetNextToken();

            // Assert
            token.Start.Should().Be(0);
            token.End.Should().Be(1);
            token.Type.Should().Be(TokenType.UNKNOWN);
        }
    }
}
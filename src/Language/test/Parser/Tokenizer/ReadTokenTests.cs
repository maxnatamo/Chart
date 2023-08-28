using System.Text;

namespace Chart.Language.Parsers.Tests
{
    public class ReadTokenTests
    {
        [Theory]
        [InlineData("false", TokenType.BOOLEAN, 0, 5, "false")]
        [InlineData("true", TokenType.BOOLEAN, 0, 4, "true")]
        [InlineData("FALSE", TokenType.NAME, 0, 5, "FALSE")]
        [InlineData(@" ""FALSE"" ", TokenType.STRING, 1, 8, "FALSE")]
        [InlineData("null", TokenType.NULL, 0, 4)]
        [InlineData("NULL", TokenType.NAME, 0, 4, "NULL")]
        [InlineData(@" ""null"" ", TokenType.STRING, 1, 7, "null")]
        [InlineData("&", TokenType.AMPERSAND, 0, 1, "&")]
        [InlineData("|", TokenType.PIPE, 0, 1, "|")]
        [InlineData("$", TokenType.DOLLAR_SIGN, 0, 1, "$")]
        [InlineData("@", TokenType.AT, 0, 1, "@")]
        [InlineData("=", TokenType.EQUAL, 0, 1, "=")]
        [InlineData("!", TokenType.EXCLAMATION_POINT, 0, 1, "!")]
        [InlineData(":", TokenType.COLON, 0, 1, ":")]
        [InlineData("(", TokenType.PARENTHESIS_LEFT, 0, 1, "(")]
        [InlineData(")", TokenType.PARENTHESIS_RIGHT, 0, 1, ")")]
        [InlineData("{", TokenType.BRACE_LEFT, 0, 1, "{")]
        [InlineData("}", TokenType.BRACE_RIGHT, 0, 1, "}")]
        [InlineData("[", TokenType.BRACKET_LEFT, 0, 1, "[")]
        [InlineData("]", TokenType.BRACKET_RIGHT, 0, 1, "]")]
        [InlineData("...", TokenType.SPREAD, 0, 3)]
        [InlineData("/", TokenType.UNKNOWN, 0, 1, "/")]
        public void ReadToken_ReturnsCorrectToken_GivenTokenString(string source, TokenType type, int start, int end, string value = "")
        {
            // Arrange

            // Act
            Token token = new Tokenizer(source).GetNextToken();

            // Assert
            token.Start.Should().Be(start);
            token.End.Should().Be(end);
            token.Type.Should().Be(type);
            token.Value.Should().Be(value);
        }

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
    }
}
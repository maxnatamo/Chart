namespace Chart.Language.Tests
{
    public class ParserTests
    {
        [Fact]
        public void ExpectTokenTypeGivenFailedAssertionThrowsUnexpectedTokenException()
        {
            // Arrange
            string source = "name { }";
            SchemaParser schemaParser = new SchemaParser();

            schemaParser.Tokenizer.SetSource(source);
            schemaParser.CurrentToken = schemaParser.Tokenizer.GetNextToken();

            // Act
            Action act = () => schemaParser.Expect(TokenType.AT);

            // Assert
            act.Should().Throw<UnexpectedTokenException>();
        }

        [Fact]
        public void ExpectValueGivenFailedAssertionThrowsUnexpectedTokenException()
        {
            // Arrange
            string source = "name { }";
            SchemaParser schemaParser = new SchemaParser();

            schemaParser.Tokenizer.SetSource(source);
            schemaParser.CurrentToken = schemaParser.Tokenizer.GetNextToken();

            // Act
            Action act = () => schemaParser.Expect("age");

            // Assert
            act.Should().Throw<UnexpectedTokenException>();
        }
    }
}
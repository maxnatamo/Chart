namespace Chart.Language.Parsers.Tests
{
    public class OperationTokenTests
    {
        [Fact]
        public void QueryOperationReturnsQueryWithoutName()
        {
            // Arrange
            string source = "query";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            Token token = tokenizer.GetNextToken();

            // Assert
            token.Start.Should().Be(0);
            token.End.Should().Be(5);
            token.Type.Should().Be(TokenType.NAME);
            token.Value.Should().Be("query");
        }

        [Fact]
        public void QueryOperationWithNameReturnsNamedQuery()
        {
            // Arrange
            string source = "query queryName";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = new List<Token>
            {
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken(),
            };

            // Assert
            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(5);
            tokens[0].Type.Should().Be(TokenType.NAME);
            tokens[0].Value.Should().Be("query");

            tokens[1].Start.Should().Be(6);
            tokens[1].End.Should().Be(15);
            tokens[1].Type.Should().Be(TokenType.NAME);
            tokens[1].Value.Should().Be("queryName");
        }

        [Fact]
        public void MutationOperationReturnsMutationWithoutName()
        {
            // Arrange
            string source = "mutation";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            Token token = tokenizer.GetNextToken();

            // Assert
            token.Start.Should().Be(0);
            token.End.Should().Be(8);
            token.Type.Should().Be(TokenType.NAME);
            token.Value.Should().Be("mutation");
        }

        [Fact]
        public void SubscriptionOperationReturnsSubscriptionWithoutName()
        {
            // Arrange
            string source = "subscription";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            Token token = tokenizer.GetNextToken();

            // Assert
            token.Start.Should().Be(0);
            token.End.Should().Be(12);
            token.Type.Should().Be(TokenType.NAME);
            token.Value.Should().Be("subscription");
        }
    }
}
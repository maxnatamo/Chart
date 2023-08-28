namespace Chart.Language.Parsers.Tests
{
    public class NumberTokenTests
    {
        [Fact]
        public void TokenWithMultipleDotsReturnUnknownToken()
        {
            // Arrange
            string source = "1.2.3";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = new List<Token>
            {
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken(),
            };

            // Assert
            tokens.Count.Should().Be(2);

            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(5);
            tokens[0].Type.Should().Be(TokenType.UNKNOWN);
            tokens[0].Value.Should().Be("1.2.3");
        }

        [Fact]
        public void TokenWithSingleDigitsReturnsFloatToken()
        {
            // Arrange
            string source = "123.2";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = new List<Token>
            {
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken(),
            };

            // Assert
            tokens.Count.Should().Be(2);

            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(5);
            tokens[0].Type.Should().Be(TokenType.FLOAT);
            tokens[0].Value.Should().Be("123.2");
        }

        [Fact]
        public void TokenWithNoDigitsReturnsIntToken()
        {
            // Arrange
            string source = "123.";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = new List<Token>
            {
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken(),
            };

            // Assert
            tokens.Count.Should().Be(2);

            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(4);
            tokens[0].Type.Should().Be(TokenType.FLOAT);
            tokens[0].Value.Should().Be("123");
        }

        [Fact]
        public void TokenWithMultipleNegatesReturnsUnknownToken()
        {
            // Arrange
            string source = "--123.";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = new List<Token>
            {
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken(),
            };

            // Assert
            tokens.Count.Should().Be(2);

            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(6);
            tokens[0].Type.Should().Be(TokenType.UNKNOWN);
            tokens[0].Value.Should().Be("--123.");
        }

        [Fact]
        public void TokenWithSingleNegateReturnsNegativeFloatToken()
        {
            // Arrange
            string source = "-123.1";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = new List<Token>
            {
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken(),
            };

            // Assert
            tokens.Count.Should().Be(2);

            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(6);
            tokens[0].Type.Should().Be(TokenType.FLOAT);
            tokens[0].Value.Should().Be("-123.1");
        }

        [Fact]
        public void TokenWithExponentReturnsFloatToken()
        {
            // Arrange
            string source = "1.50E-15";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = new List<Token>
            {
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken(),
            };

            // Assert
            tokens.Count.Should().Be(2);

            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(8);
            tokens[0].Type.Should().Be(TokenType.FLOAT);
            tokens[0].Value.Should().Be("1.5E-15");
        }

        [Fact]
        public void TokenWithNegativeIntegerReturnsIntToken()
        {
            // Arrange
            string source = "-12";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            List<Token> tokens = new List<Token>
            {
                tokenizer.GetNextToken(),
                tokenizer.GetNextToken(),
            };

            // Assert
            tokens.Count.Should().Be(2);

            tokens[0].Start.Should().Be(0);
            tokens[0].End.Should().Be(3);
            tokens[0].Type.Should().Be(TokenType.INT);
            tokens[0].Value.Should().Be("-12");
        }
    }
}
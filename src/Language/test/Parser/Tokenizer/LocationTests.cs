namespace Chart.Language.Parsers.Tests
{
    public class LocationTests
    {
        [Fact]
        public void ReadToken_ReturnsCorrectLocation_GivenSingleLineQuery()
        {
            // Arrange
            string source = "query { file }";

            // Act
            List<Token> tokens = new Tokenizer(source).GetAllTokens();

            // Assert
            tokens.Should().SatisfyRespectively(
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(1, 1)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(1, 7)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(1, 9)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(1, 14))
            );
        }

        [Fact]
        public void ReadToken_ReturnsCorrectLocation_GivenMultiLineQuery()
        {
            // Arrange
            string source = "query {\n"
                          + "  file\n"
                          + "}";

            // Act
            List<Token> tokens = new Tokenizer(source).GetAllTokens();

            // Assert
            tokens.Should().SatisfyRespectively(
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(1, 1)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(1, 7)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(2, 3)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(3, 1))
            );
        }

        [Fact]
        public void ReadToken_ReturnsCorrectLocation_GivenIndentedMultiLineQuery()
        {
            // Arrange
            string source = @"query {
                              file
                            }";

            // Act
            List<Token> tokens = new Tokenizer(source).GetAllTokens();

            // Assert
            tokens.Should().SatisfyRespectively(
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(1, 1)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(1, 7)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(2, 31)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(3, 29))
            );
        }

        [Fact]
        public void ReadToken_ReturnsCorrectLocation_GivenQueryWithArguments()
        {
            // Arrange
            string source = "query {\n"
                          + "  file(maxSize: 1400)\n"
                          + "}";

            // Act
            List<Token> tokens = new Tokenizer(source).GetAllTokens();

            // Assert
            tokens.Should().SatisfyRespectively(
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(1, 1)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(1, 7)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(2, 3)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(2, 7)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(2, 8)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(2, 15)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(2, 17)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(2, 21)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(3, 1))
            );
        }

        [Fact]
        public void ReadToken_ReturnsCorrectLocation_GivenMultipleNewlines()
        {
            // Arrange
            string source = "query {\n"
                          + "\n"
                          + "\n"
                          + "  file\n"
                          + "}";

            // Act
            List<Token> tokens = new Tokenizer(source).GetAllTokens();

            // Assert
            tokens.Should().SatisfyRespectively(
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(1, 1)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(1, 7)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(4, 3)),
                token => token.Location.Should().BeEquivalentTo(new GraphLocation(5, 1))
            );
        }
    }
}
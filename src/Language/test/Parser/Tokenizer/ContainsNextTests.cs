namespace Chart.Language.Parsers.Tests
{
    public class ContainsNextTests
    {
        [Fact]
        public void ContainsNextGivenNonWholeWordWithoutWholeWordReturnsTrue()
        {
            // Arrange
            string source = "LongString";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            bool contains = new Tokenizer(source).ContainsNext("Long", false);

            // Assert
            contains.Should().BeTrue();
        }

        [Fact]
        public void ContainsNextGivenNonWholeWordWithWholeWordReturnsFalse()
        {
            // Arrange
            string source = "LongString";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            bool contains = new Tokenizer(source).ContainsNext("Long", true);

            // Assert
            contains.Should().BeFalse();
        }
    }
}
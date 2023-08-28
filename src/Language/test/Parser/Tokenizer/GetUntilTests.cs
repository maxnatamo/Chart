namespace Chart.Language.Parsers.Tests
{
    public class GetUntilTests
    {
        [Fact]
        public void GetUntilReturnsStringUntilEndGivenSourceWithoutEnd()
        {
            // Arrange
            string source = "\"\"\"Description without end";
            Tokenizer tokenizer = new Tokenizer(source);

            // Act
            string value = tokenizer.GetUntil(str => !str.StartsWith("\"\"\""));

            // Assert
            value.Should().Be("\"\"\"Description without end");
        }
    }
}
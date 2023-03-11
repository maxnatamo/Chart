namespace Chart.Core.Parsers.Tests
{
    public class IndexNotOfTests
    {
        [Fact]
        public void IndexNotOfReturnsTheIndexOfFirstCharacter()
        {
            // Arrange
            string str = "   a";
            char c = ' ';

            // Act
            int idx = str.IndexNotOf(c);

            // Assert
            idx.Should().Be(3);
        }

        [Fact]
        public void IndexNotAnyOfReturnsTheIndexOfFirstCharacter()
        {
            // Arrange
            string str = "  \ta";
            char[] c = { ' ', '\t', '\n', '\r' };

            // Act
            int idx = str.IndexNotOfAny(c);

            // Assert
            idx.Should().Be(3);
        }
    }
}
using static Chart.Core.NameFormattingBehaviour;

namespace Chart.Core.Tests.Naming.NameFormatterTests
{
    public class FormatTypeNameTests
    {
        [Fact]
        public void FormatTypeName_FormatsNothing_GivenNoBehaviourFlags()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(0);

            // Act
            string result = formatter.FormatTypeName("QueryOperation");

            // Assert
            result.Should().Be("QueryOperation");
        }

        [Fact]
        public void FormatTypeName_ReturnsEmptyString_GivenNull()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(0);

            // Act
            string result = formatter.FormatTypeName(null!);

            // Assert
            result.Should().Be(string.Empty);
        }

        [Fact]
        public void FormatTypeName_ReturnsEmptyString_GivenEmptyString()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(0);

            // Act
            string result = formatter.FormatTypeName(string.Empty);

            // Assert
            result.Should().Be(string.Empty);
        }

        [Fact]
        public void FormatPascalCase_ReturnsPascalCase_GivenPascalCaseString()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(PascalCase);

            // Act
            string result = formatter.FormatTypeName("QueryOperation");

            // Assert
            result.Should().Be("queryOperation");
        }

        [Fact]
        public void FormatPascalCase_ReturnsPascalCase_GivenUpperCaseString()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(PascalCase);

            // Act
            string result = formatter.FormatTypeName("QUERYOPERATION");

            // Assert
            result.Should().Be("qUERYOPERATION");
        }

        [Fact]
        public void FormatPascalCase_ReturnsName_GivenSingleLetter()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(PascalCase);

            // Act
            string result = formatter.FormatTypeName("A");

            // Assert
            result.Should().Be("A");
        }

        [Fact]
        public void FormatCamelCase_ReturnsName_GivenSingleLetter()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(CamelCase);

            // Act
            string result = formatter.FormatTypeName("A");

            // Assert
            result.Should().Be("A");
        }

        [Fact]
        public void FormatCamelCase_ReturnsPascalCase_GivenPascalCaseString()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(CamelCase);

            // Act
            string result = formatter.FormatTypeName("queryOperation");

            // Assert
            result.Should().Be("QueryOperation");
        }

        [Fact]
        public void RemoveAsyncPostfix_ReturnsName_GivenShortName()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(RemoveAsyncPostfix);

            // Act
            string result = formatter.FormatTypeName("AAAAA");

            // Assert
            result.Should().Be("AAAAA");
        }

        [Fact]
        public void RemoveAsyncPostfix_ReturnsName_GivenNameWithoutAsync()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(RemoveAsyncPostfix);

            // Act
            string result = formatter.FormatTypeName("AAAAAAA");

            // Assert
            result.Should().Be("AAAAAAA");
        }

        [Fact]
        public void RemoveAsyncPostfix_ReturnsNameWithoutAsync_GivenLetterWithAsync()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(RemoveAsyncPostfix);

            // Act
            string result = formatter.FormatTypeName("bAsync");

            // Assert
            result.Should().Be("b");
        }

        [Fact]
        public void RemoveAsyncPostfix_ReturnsName_GivenAsync()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(RemoveAsyncPostfix);

            // Act
            string result = formatter.FormatTypeName("Async");

            // Assert
            result.Should().Be("Async");
        }

        [Fact]
        public void RemoveGetPrefix_ReturnsName_GivenSingleLetter()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(RemoveGetPrefix);

            // Act
            string result = formatter.FormatTypeName("A");

            // Assert
            result.Should().Be("A");
        }

        [Fact]
        public void RemoveGetPrefix_ReturnsName_GivenShortName()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(RemoveGetPrefix);

            // Act
            string result = formatter.FormatTypeName("AAA");

            // Assert
            result.Should().Be("AAA");
        }

        [Fact]
        public void RemoveGetPrefix_ReturnsName_GivenGet()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(RemoveGetPrefix);

            // Act
            string result = formatter.FormatTypeName("Get");

            // Assert
            result.Should().Be("Get");
        }

        [Fact]
        public void RemoveGetPrefix_ReturnsName_GivenGetter()
        {
            // Arrage
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(RemoveGetPrefix);

            // Act
            string result = formatter.FormatTypeName("GetName");

            // Assert
            result.Should().Be("Name");
        }

        [Fact]
        public void RemoveGetPrefix_ReturnsName_GivenLowercaseGetter()
        {
            // Arrage
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(RemoveGetPrefix);

            // Act
            string result = formatter.FormatTypeName("getName");

            // Assert
            result.Should().Be("Name");
        }

        [Theory]
        [InlineData("Get", "get")]
        [InlineData("Async", "async")]
        [InlineData("name", "name")]
        [InlineData("Name", "name")]
        [InlineData("GetName", "name")]
        [InlineData("GetNameAsync", "name")]
        [InlineData("GetAuthorName", "authorName")]
        [InlineData("GetAuthorNameAsync", "authorName")]
        public void FormatTypeName_ReturnsFormattedName_GivenName(string source, string expected)
        {
            // Arrage
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(RemoveGetPrefix | PascalCase | RemoveAsyncPostfix);

            // Act
            string result = formatter.FormatTypeName(source);

            // Assert
            result.Should().Be(expected);
        }
    }
}
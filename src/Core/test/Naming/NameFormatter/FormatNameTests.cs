namespace Chart.Core.Tests.Naming.NameFormattedTests
{
    public class FormatNameTests
    {
        [Fact]
        public void FormatName_FormatsNothing_GivenNoBehaviourFlags()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(0);

            // Act
            string result = formatter.FormatName("QueryOperation");

            // Assert
            result.Should().Be("QueryOperation");
        }

        [Fact]
        public void FormatName_ReturnsEmptyString_GivenNull()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(0);

            // Act
            string result = formatter.FormatName(null!);

            // Assert
            result.Should().Be(string.Empty);
        }

        [Fact]
        public void FormatName_ReturnsEmptyString_GivenEmptyString()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(0);

            // Act
            string result = formatter.FormatName(string.Empty);

            // Assert
            result.Should().Be(string.Empty);
        }

        [Fact]
        public void FormatPascalCase_ReturnsPascalCase_GivenPascalCaseString()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(NameFormattingBehaviour.PascalCase);

            // Act
            string result = formatter.FormatName("QueryOperation");

            // Assert
            result.Should().Be("queryOperation");
        }

        [Fact]
        public void FormatPascalCase_ReturnsPascalCase_GivenUpperCaseString()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(NameFormattingBehaviour.PascalCase);

            // Act
            string result = formatter.FormatName("QUERYOPERATION");

            // Assert
            result.Should().Be("qUERYOPERATION");
        }

        [Fact]
        public void FormatPascalCase_ReturnsName_GivenSingleLetter()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(NameFormattingBehaviour.PascalCase);

            // Act
            string result = formatter.FormatName("A");

            // Assert
            result.Should().Be("A");
        }

        [Fact]
        public void RemoveAsyncPostfix_ReturnsName_GivenShortName()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(NameFormattingBehaviour.RemoveAsyncPostfix);

            // Act
            string result = formatter.FormatName("AAAAA");

            // Assert
            result.Should().Be("AAAAA");
        }

        [Fact]
        public void RemoveAsyncPostfix_ReturnsName_GivenNameWithoutAsync()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(NameFormattingBehaviour.RemoveAsyncPostfix);

            // Act
            string result = formatter.FormatName("AAAAAAA");

            // Assert
            result.Should().Be("AAAAAAA");
        }

        [Fact]
        public void RemoveAsyncPostfix_ReturnsNameWithoutAsync_GivenLetterWithAsync()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(NameFormattingBehaviour.RemoveAsyncPostfix);

            // Act
            string result = formatter.FormatName("bAsync");

            // Assert
            result.Should().Be("b");
        }

        [Fact]
        public void RemoveAsyncPostfix_ReturnsName_GivenAsync()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(NameFormattingBehaviour.RemoveAsyncPostfix);

            // Act
            string result = formatter.FormatName("Async");

            // Assert
            result.Should().Be("Async");
        }

        [Fact]
        public void RemoveGetPrefix_ReturnsName_GivenSingleLetter()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(NameFormattingBehaviour.RemoveGetPrefix);

            // Act
            string result = formatter.FormatName("A");

            // Assert
            result.Should().Be("A");
        }

        [Fact]
        public void RemoveGetPrefix_ReturnsName_GivenShortName()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(NameFormattingBehaviour.RemoveGetPrefix);

            // Act
            string result = formatter.FormatName("AAA");

            // Assert
            result.Should().Be("AAA");
        }

        [Fact]
        public void RemoveGetPrefix_ReturnsName_GivenGet()
        {
            // Arrange
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(NameFormattingBehaviour.RemoveGetPrefix);

            // Act
            string result = formatter.FormatName("Get");

            // Assert
            result.Should().Be("Get");
        }

        [Fact]
        public void RemoveGetPrefix_ReturnsName_GivenGetter()
        {
            // Arrage
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(NameFormattingBehaviour.RemoveGetPrefix);

            // Act
            string result = formatter.FormatName("GetName");

            // Assert
            result.Should().Be("Name");
        }

        [Fact]
        public void RemoveGetPrefix_ReturnsName_GivenLowercaseGetter()
        {
            // Arrage
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(NameFormattingBehaviour.RemoveGetPrefix);

            // Act
            string result = formatter.FormatName("getName");

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
        public void FormatName_ReturnsFormattedName_GivenName(string source, string expected)
        {
            // Arrage
            NameFormatter formatter = new NameFormatter();
            formatter.SetOptions(NameFormattingBehaviour.RemoveGetPrefix |
                                 NameFormattingBehaviour.PascalCase |
                                 NameFormattingBehaviour.RemoveAsyncPostfix);

            // Act
            string result = formatter.FormatName(source);

            // Assert
            result.Should().Be(expected);
        }
    }
}
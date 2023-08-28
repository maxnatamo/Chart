namespace Chart.Language.Tests
{
    public class CommentTests
    {
        [Fact]
        public void SingleQuoteDescriptionOnTypeReturnsTypeWithDescription()
        {
            // Arrange
            string source = @"""Test type"" type Test { }";

            // Act
            GraphDocument document = new SchemaParser().ParseSchema(source);

            // Assert
            document.Should().NotBeNull();
            document.Definitions.Should().NotBeEmpty();
            document.Definitions[0].DefinitionKind.Should().Be(GraphDefinitionKind.Type);
            document.Definitions[0].Description.Should().NotBeNull();
            document.Definitions[0].Description?.Value.Should().Be("Test type");
        }

        [Fact]
        public void BlockQuoteDescriptionOnTypeReturnsTypeWithDescription()
        {
            // Arrange
            string source = @"
            """"""
            TODO: Add description!
            """"""
            type Test
            {

            }";

            // Act
            GraphDocument document = new SchemaParser().ParseSchema(source);

            // Assert
            document.Should().NotBeNull();
            document.Definitions.Should().NotBeEmpty();
            document.Definitions[0].DefinitionKind.Should().Be(GraphDefinitionKind.Type);
            document.Definitions[0].Description.Should().NotBeNull();
            document.Definitions[0].Description?.Value.Should().Be("TODO: Add description!");
        }

        [Fact]
        public void BlockQuoteDescriptionWithQuotesOnTypeReturnsTypeWithDescription()
        {
            // Arrange
            string source = @"
            """"""
            Congrats on your ""baby""
            """"""
            type Test
            {

            }";

            // Act
            GraphDocument document = new SchemaParser().ParseSchema(source);

            // Assert
            document.Should().NotBeNull();
            document.Definitions.Should().NotBeEmpty();
            document.Definitions[0].DefinitionKind.Should().Be(GraphDefinitionKind.Type);
            document.Definitions[0].Description.Should().NotBeNull();
            document.Definitions[0].Description?.Value.Should().Be(@"Congrats on your ""baby""");
        }

        [Fact]
        public void BlockQuoteDescriptionWithNewlinesOnTypeReturnsTypeWithDescription()
        {
            // Arrange
            string source = @"
            """"""
            Line 1
            Newline!
            """"""
            type Test
            {

            }";

            // Act
            GraphDocument document = new SchemaParser().ParseSchema(source);

            // Assert
            document.Should().NotBeNull();
            document.Definitions.Should().NotBeEmpty();
            document.Definitions[0].DefinitionKind.Should().Be(GraphDefinitionKind.Type);
            document.Definitions[0].Description.Should().NotBeNull();
            document.Definitions[0].Description?.Value.Should().Be($"Line 1{System.Environment.NewLine}Newline!");
        }
    }
}
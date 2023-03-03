namespace Chart.Core.Validation.Tests
{
    public class DocumentTests
    {
        [Fact]
        public void TryValidateReturnsTrueWithOperation()
        {
            // Arrange
            string source = "query getDogName { dog { name } }";

            GraphDocument document = new SchemaParser().Parse(source);

            // Act
            bool isValid = new Validator().TryValidate(document);

            // Assert
            isValid.Should().BeTrue();
        }

        [Fact]
        public void TryValidateReturnsFalseWithTypeDefinition()
        {
            // Arrange
            string source = "type Dog { name: String }";

            GraphDocument document = new SchemaParser().Parse(source);

            // Act
            bool isValid = new Validator().TryValidate(document);

            // Assert
            isValid.Should().BeFalse();
        }

        [Fact]
        public void TryValidateReturnsFalseWithNonUniqueOperationName()
        {
            // Arrange
            string source = "query getDogName { name } mutation getDogName { name }";

            GraphDocument document = new SchemaParser().Parse(source);

            // Act
            bool isValid = new Validator().TryValidate(document);

            // Assert
            isValid.Should().BeFalse();
        }

        [Fact]
        public void TryValidateReturnsTrueWithLoneAnonymousOperation()
        {
            // Arrange
            string source = "{ dog { name } }";

            GraphDocument document = new SchemaParser().Parse(source);

            // Act
            bool isValid = new Validator().TryValidate(document);

            // Assert
            isValid.Should().BeTrue();
        }

        [Fact]
        public void TryValidateReturnsFalseWithAnonymousAndNamedOperation()
        {
            // Arrange
            string source = "{ dog { name } } query getDogName { dog name }";

            GraphDocument document = new SchemaParser().Parse(source);

            // Act
            bool isValid = new Validator().TryValidate(document);

            // Assert
            isValid.Should().BeFalse();
        }

        [Fact]
        public void TryValidateReturnsTrueWithLoneFragment()
        {
            // Arrange
            string source = "fragment frag on Dog { name }";

            GraphDocument document = new SchemaParser().Parse(source);

            // Act
            bool isValid = new Validator().TryValidate(document);

            // Assert
            isValid.Should().BeTrue();
        }

        [Fact]
        public void TryValidateReturnsFalseWithNonUniqueFragment()
        {
            // Arrange
            string source = "fragment frag on Dog { name } fragment frag on Dog { name }";

            GraphDocument document = new SchemaParser().Parse(source);

            // Act
            bool isValid = new Validator().TryValidate(document);

            // Assert
            isValid.Should().BeFalse();
        }
    }
}
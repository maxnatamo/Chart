namespace Chart.Utilities.Tests.GraphNameTests
{
    public class EqualsTests
    {
        [Fact]
        public void Equals_ShouldReturnTrue_GivenSameReference()
        {
            // Arrange
            GraphName name = new("test");

            // Act
            bool equals = name.Equals(name);

            // Assert
            equals.Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnTrue_GivenSameContent()
        {
            // Arrange
            GraphName name1 = new("test");
            GraphName name2 = new("test");

            // Act
            bool equals = name1.Equals(name2);

            // Assert
            equals.Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_GivenNullInstance()
        {
            // Arrange
            GraphName name1 = new("test");
            GraphName? name2 = null;

            // Act
            bool equals = name1.Equals(name2);

            // Assert
            equals.Should().BeFalse();
        }

        [Fact]
        public void Equals_ShouldReturnTrue_GivenSameStringValue()
        {
            // Arrange
            GraphName name1 = new("test");
            string name2 = "test";

            // Act
            bool equals = name1.Equals(name2);

            // Assert
            equals.Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_GivenInvalidObject()
        {
            // Arrange
            GraphName name1 = new("test");
            int name2 = 4;

            // Act
            bool equals = name1.Equals(name2);

            // Assert
            equals.Should().BeFalse();
        }

        [Fact]
        public void EqualsOperator_ShouldReturnTrue_GivenSameValue()
        {
            // Arrange
            GraphName name1 = new("test");
            GraphName name2 = new("test");

            // Act
            bool equals = name1 == name2;

            // Assert
            equals.Should().BeTrue();
        }

        [Fact]
        public void EqualsOperator_ShouldReturnFalse_GivenNullValue()
        {
            // Arrange
            GraphName name1 = new("test");
            GraphName? name2 = null;

            // Act
            bool equals = name1 == name2;

            // Assert
            equals.Should().BeFalse();
        }

        [Fact]
        public void NotEqualsOperator_ShouldReturnFalse_GivenSameValue()
        {
            // Arrange
            GraphName name1 = new("test");
            GraphName name2 = new("test");

            // Act
            bool equals = name1 != name2;

            // Assert
            equals.Should().BeFalse();
        }

        [Fact]
        public void NotEqualsOperator_ShouldReturnTrue_GivenNullValue()
        {
            // Arrange
            GraphName name1 = new("test");
            GraphName? name2 = null;

            // Act
            bool equals = name1 != name2;

            // Assert
            equals.Should().BeTrue();
        }
    }
}
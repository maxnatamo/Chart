namespace Chart.Core.TypeResolver.Tests
{
    public class RegisterTypeTests
    {
        [Theory]
        [InlineData(typeof(Boolean))]
        [InlineData(typeof(Single))]
        [InlineData(typeof(Double))]
        [InlineData(typeof(Decimal))]
        [InlineData(typeof(SByte))]
        [InlineData(typeof(Int16))]
        [InlineData(typeof(Int32))]
        [InlineData(typeof(Int64))]
        [InlineData(typeof(Int128))]
        [InlineData(typeof(Byte))]
        [InlineData(typeof(UInt16))]
        [InlineData(typeof(UInt32))]
        [InlineData(typeof(UInt64))]
        [InlineData(typeof(UInt128))]
        [InlineData(typeof(String))]
        public void RegisterTypeHasGraphScalarTypes(Type scalarType)
        {
            // Arrange
            Resolver resolver = new Resolver();

            // Act

            // Assert
            resolver.Visited(scalarType).Should().BeTrue();
        }

        [Fact]
        public void RegisterTypeReturnsFalseWithoutVisitedObjectType()
        {
            // Arrange
            Resolver resolver = new Resolver();

            // Act

            // Assert
            resolver.Visited<Book>().Should().BeFalse();
        }

        [Fact]
        public void RegisterTypeReturnsTrueWithVisitedObjectType()
        {
            // Arrange
            Resolver resolver = new Resolver();

            // Act
            resolver.RegisterType<Book>();

            // Assert
            resolver.Visited<Book>().Should().BeTrue();
        }

        [Fact]
        public void RegisterTypeReturnsTrueWithUnderlyingListType()
        {
            // Arrange
            Resolver resolver = new Resolver();
            List<Book> values = new List<Book>();

            // Act
            resolver.RegisterType(values);

            // Assert
            resolver.Visited<Book>().Should().BeTrue();
        }

        [Fact]
        public void RegisterTypeReturnsTrueWithNestedObjectType()
        {
            // Arrange
            Resolver resolver = new Resolver();
            Book obj = new Book();

            // Act
            resolver.RegisterType(obj);

            // Assert
            resolver.Visited<Book>().Should().BeTrue();
            resolver.Visited<Author>().Should().BeTrue();
        }
    }
}
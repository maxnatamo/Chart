using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core.Tests.Types.TypeRegistryTests
{
    public class VisitTests
    {
        [Fact]
        public void Visit_ReturnsTrue_GivenScalarType()
        {
            // Arrange
            ITypeRegistry typeRegistry = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<ITypeRegistry>();

            // Act
            bool visited = typeRegistry.Visited<IntType>();

            // Assert
            visited.Should().BeTrue();
        }

        [Fact]
        public void Visit_ReturnsTrue_GivenNonNullScalarType()
        {
            // Arrange
            ITypeRegistry typeRegistry = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<ITypeRegistry>();

            // Act
            bool visited = typeRegistry.Visited<NonNullType<IntType>>();

            // Assert
            visited.Should().BeTrue();
        }

        [Fact]
        public void Visit_ReturnsTrue_GivenListScalarType()
        {
            // Arrange
            ITypeRegistry typeRegistry = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<ITypeRegistry>();

            // Act
            bool visited = typeRegistry.Visited<ListType<IntType>>();

            // Assert
            visited.Should().BeTrue();
        }

        [Fact]
        public void Visit_ReturnsTrue_GivenNestedCompositeScalarType()
        {
            // Arrange
            ITypeRegistry typeRegistry = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<ITypeRegistry>();

            // Act
            bool visited = typeRegistry.Visited<NonNullType<ListType<NonNullType<IntType>>>>();

            // Assert
            visited.Should().BeTrue();
        }

        [Theory]
        [InlineData("Boolean")]
        [InlineData("String")]
        [InlineData("Single")]
        [InlineData("Float")]
        [InlineData("Decimal")]
        [InlineData("Long")]
        [InlineData("Int")]
        [InlineData("Short")]
        [InlineData("Byte")]
        public void Visit_ReturnsTrue_GivenSchemaTypes(string schemaType)
        {
            // Arrange
            ITypeRegistry typeRegistry = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<ITypeRegistry>();

            // Act
            bool visited = typeRegistry.Visited(schemaType);

            // Assert
            visited.Should().BeTrue();
        }

        [Theory]
        [InlineData("Boolean")]
        [InlineData("String")]
        [InlineData("Single")]
        [InlineData("Double")]
        [InlineData("Decimal")]
        [InlineData("Int64")]
        [InlineData("Int32")]
        [InlineData("Int16")]
        [InlineData("SByte")]
        public void Visit_ReturnsTrue_GivenTypeRuntimeName(string runtimeType)
        {
            // Arrange
            ITypeRegistry typeRegistry = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<ITypeRegistry>();

            // Act
            bool visited = typeRegistry.Visited(runtimeType);

            // Assert
            visited.Should().BeTrue();
        }

        [Theory]
        [InlineData("BooleanType")]
        [InlineData("StringType")]
        [InlineData("SingleType")]
        [InlineData("FloatType")]
        [InlineData("DecimalType")]
        [InlineData("LongType")]
        [InlineData("IntType")]
        [InlineData("ShortType")]
        [InlineData("ByteType")]
        public void Visit_ReturnsTrue_GivenScalarTypeName(string scalarTypeName)
        {
            // Arrange
            ITypeRegistry typeRegistry = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<ITypeRegistry>();

            // Act
            bool visited = typeRegistry.Visited(scalarTypeName);

            // Assert
            visited.Should().BeTrue();
        }
    }
}
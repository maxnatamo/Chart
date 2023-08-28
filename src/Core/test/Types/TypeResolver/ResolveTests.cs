using Chart.Language.SyntaxTree;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core.Tests.Types.TypeResolverTests
{
    public class ResolveTests
    {
        [Theory]
        [InlineData(typeof(Boolean))]
        [InlineData(typeof(String))]
        [InlineData(typeof(Single))]
        [InlineData(typeof(Double))]
        [InlineData(typeof(Decimal))]
        [InlineData(typeof(Int64))]
        [InlineData(typeof(Int32))]
        [InlineData(typeof(Int16))]
        [InlineData(typeof(SByte))]
        public void Visit_ReturnsTrue_GivenScalarType(Type scalarType)
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeResolver typeResolver = services.GetRequiredService<ITypeResolver>();

            // Act
            Action act = () => typeResolver.Resolve(scalarType);

            // Assert
            act.Should().NotThrow();
        }

        [Theory]
        [InlineData(typeof(Boolean?), "Boolean")]
        [InlineData(typeof(Optional<String>), "String")]
        public void Visit_ReturnsTrue_GivenOptionalScalarType(Type scalarType, string expectedName)
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeResolver typeResolver = services.GetRequiredService<ITypeResolver>();

            // Act
            GraphType result = typeResolver.Resolve(scalarType);

            // Assert
            result.Should().BeOfType<GraphNamedType>();
            result.As<GraphNamedType>().Name.Should().Be(expectedName);
        }

        [Fact]
        public void Visit_ReturnsListType_GivenListType()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeResolver typeResolver = services.GetRequiredService<ITypeResolver>();

            // Act
            GraphType result = typeResolver.Resolve<IEnumerable<string>>();

            // Assert
            result.Should().BeOfType<GraphListType>();
            result.As<GraphListType>().UnderlyingType.Should().BeOfType<GraphNamedType>();
            result.As<GraphListType>().UnderlyingType.As<GraphNamedType>().Name.Should().Be("String");
        }
    }
}
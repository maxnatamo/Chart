using Chart.Models.AST;
using Chart.Shared.Exceptions;

namespace Chart.Core.TypeResolver.Tests
{
    public class ResolveTypeTests
    {
        [Fact]
        public void ResolveTypeReturnsGraphTypeWhenPassingScalarType()
        {
            // Arrange
            Resolver resolver = new Resolver();

            // Act

            // Assert
            resolver.ResolveType<String>()
                .As<GraphNamedType>().Name.Value.Should().Be("String");
        }

        [Fact]
        public void ResolveTypeReturnsGraphTypeWhenPassingRegisteredType()
        {
            // Arrange
            Resolver resolver = new Resolver();

            // Act
            resolver.RegisterType<Book>();

            // Assert
            resolver.ResolveType<Book>()
                .As<GraphNamedType>().Name.Value.Should().Be("Book");
        }

        [Fact]
        public void ResolveTypeReturnsGraphTypeWhenPassingListType()
        {
            // Arrange
            Resolver resolver = new Resolver();

            // Act
            resolver.RegisterType<Book>();

            // Assert
            resolver.ResolveType<List<Book>>()
                .TypeKind.Should().Be(GraphTypeKind.List);
    
            resolver.ResolveType<List<Book>>()
                .As<GraphListType>().UnderlyingType
                .As<GraphNamedType>().Name.Value.Should().Be("Book");
        }

        [Fact]
        public void ResolveTypeThrowsInvalidTypeExceptionWhenPassingUnregistedType()
        {
            // Arrange
            Resolver resolver = new Resolver();

            // Act
            Action act = () => resolver.ResolveType<Book>();

            // Assert
            act.Should().Throw<InvalidTypeException>();
        }
    }
}
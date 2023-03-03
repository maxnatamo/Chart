using Chart.Models.AST;
using Chart.Shared.Exceptions;

namespace Chart.Core.TypeResolver.Tests
{
    public class ResolveValueTests
    {
        [Fact]
        public void ResolveValueReturnsGraphValueWhenPassingScalarType()
        {
            // Arrange
            Resolver resolver = new Resolver();

            // Act
            GraphValue value = resolver.ResolveValue("TestString");

            // Assert
            value.ValueKind.Should().Be(GraphValueKind.String);
            value.As<GraphStringValue>().Value.Should().Be("TestString");
        }

        [Fact]
        public void ResolveStringReturnsGraphTypeWhenPassingRegisteredType()
        {
            // Arrange
            Resolver resolver = new Resolver();
            resolver.RegisterType<Book>();

            // Act
            GraphValue value = resolver.ResolveValue(new Book());

            // Assert
            value.ValueKind.Should().Be(GraphValueKind.Object);
            value.As<GraphObjectValue>().Fields.Should().NotBeEmpty();
        }

        [Fact]
        public void ResolveValueReturnsGraphValueWhenPassingListType()
        {
            // Arrange
            Resolver resolver = new Resolver();
            resolver.RegisterType<Book>();

            // Act
            GraphValue value = resolver.ResolveValue(new List<Book>{ new Book() });

            // Assert
            value.ValueKind.Should().Be(GraphValueKind.List);
            value.As<GraphListValue>().Values.Should().NotBeEmpty();
            value.As<GraphListValue>().Values[0].ValueKind.Should().Be(GraphValueKind.Object);
        }

        [Fact]
        public void ResolveValueThrowsInvalidTypeExceptionWhenPassingUnregistedType()
        {
            // Arrange
            Resolver resolver = new Resolver();

            // Act
            Action act = () => resolver.ResolveValue(new Book());

            // Assert
            act.Should().Throw<InvalidTypeException>();
        }
    }
}
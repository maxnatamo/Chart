using Chart.Language.SyntaxTree;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core.Tests.Types.ValueCoercerTests
{
    public class CoerceInputTests
    {
        [Fact]
        public void CoerceInput_ReturnsSameValue_GivenValueAndCorrespondingType()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            IValueCoercer valueCoercer = services.GetRequiredService<IValueCoercer>();

            // Act
            IGraphValue? result = valueCoercer.CoerceInput(
                new GraphStringValue("value"),
                new GraphNamedType("String")
            );

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GraphStringValue>();
            result.As<GraphStringValue>().Value.Should().Be("value");
        }

        [Fact]
        public void CoerceInput_ReturnsNull_GivenValueAndDifferentType()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            IValueCoercer valueCoercer = services.GetRequiredService<IValueCoercer>();

            // Act
            IGraphValue? result = valueCoercer.CoerceInput(
                new GraphStringValue("value"),
                new GraphNamedType("Boolean")
            );

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void CoerceInput_ReturnsList_GivenListValueAndListType()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            IValueCoercer valueCoercer = services.GetRequiredService<IValueCoercer>();

            // Act
            IGraphValue? result = valueCoercer.CoerceInput(
                new GraphListValue(new List<IGraphValue>
                {
                    new GraphStringValue("value")
                }),
                new GraphListType(new GraphNamedType("String"))
            );

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GraphListValue>();
            result.As<GraphListValue>().Value.Should().SatisfyRespectively(
                value =>
                {
                    value.Should().BeOfType<GraphStringValue>();
                    value.As<GraphStringValue>().Value.Should().Be("value");
                }
            );
        }

        [Fact]
        public void CoerceInput_ReturnsListWithNullValue_GivenNullableListValueWithNonCoercibleValueAndListType()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            IValueCoercer valueCoercer = services.GetRequiredService<IValueCoercer>();

            // Act
            IGraphValue? result = valueCoercer.CoerceInput(
                new GraphListValue(new List<IGraphValue>
                {
                    new GraphStringValue("value"),
                    new GraphBooleanValue(true)
                }),
                new GraphListType(new GraphNamedType("String"))
            );

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GraphListValue>();
            result.As<GraphListValue>().Value.Should().SatisfyRespectively(
                value =>
                {
                    value.Should().BeOfType<GraphStringValue>();
                    value.As<GraphStringValue>().Value.Should().Be("value");
                },
                value => value.Should().BeOfType<GraphNullValue>()
            );
        }

        [Fact]
        public void CoerceInput_ReturnsNull_GivenNonNullableListValueWithNonCoercibleValueAndListType()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            IValueCoercer valueCoercer = services.GetRequiredService<IValueCoercer>();

            // Act
            IGraphValue? result = valueCoercer.CoerceInput(
                new GraphListValue(new List<IGraphValue>
                {
                    new GraphStringValue("value"),
                    new GraphBooleanValue(true)
                }),
                new GraphListType(
                    new GraphNamedType("String")
                    {
                        NonNullable = true
                    })
            );

            // Assert
            result.Should().BeNull();
        }
    }
}
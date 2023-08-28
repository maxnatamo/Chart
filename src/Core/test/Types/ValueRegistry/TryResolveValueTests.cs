using Chart.Language.SyntaxTree;

namespace Chart.Core.Tests.Types.ValueRegistryTests
{
    public class TryResolveValueTests
    {
        [Fact]
        public void TryResolveValue_ReturnsNullValue_GivenNullObject()
        {
            // Arrange
            object? value = null;
            ValueRegistry valueRegistry = new ValueRegistry();

            // Act
            bool result = valueRegistry.TryResolveValue(value, out IGraphValue? graphValue);

            // Assert
            result.Should().BeTrue();
            graphValue.Should().NotBeNull();
            graphValue.Should().BeOfType<GraphNullValue>();
            graphValue.As<GraphNullValue>().Value.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(ValueBindingTypes))]
        public void TryResolveValue_ReturnsCorrectValue_GivenObject(object? value, Type expectedType)
        {
            // Arrange
            ValueRegistry valueRegistry = new ValueRegistry();

            // Act
            bool result = valueRegistry.TryResolveValue(value, out IGraphValue? graphValue);

            // Assert
            result.Should().BeTrue();
            graphValue.Should().NotBeNull().And.BeOfType(expectedType);
            graphValue?.Value.Should().Be(value);
        }

        [Fact]
        public void TryResolveValue_ReturnsListValue_GivenList()
        {
            // Arrange
            object? value = new List<string> { "string", "value" };
            ValueRegistry valueRegistry = new ValueRegistry();

            // Act
            bool result = valueRegistry.TryResolveValue(value, out IGraphValue? graphValue);

            // Assert
            result.Should().BeTrue();
            graphValue.Should().NotBeNull().And.BeOfType<GraphListValue>();
            graphValue.As<GraphListValue>().Value.Should()
                .HaveCount(2).And
                .AllSatisfy(v => v.Should().BeOfType<GraphStringValue>()).And
                .SatisfyRespectively(
                    value => value.As<GraphStringValue>().Value.Should().Be("string"),
                    value => value.As<GraphStringValue>().Value.Should().Be("value")
                );
        }

        [Fact]
        public void TryResolveValue_ReturnsListValue_GivenListWithNullValue()
        {
            // Arrange
            object? value = new List<string> { null! };
            ValueRegistry valueRegistry = new ValueRegistry();

            // Act
            bool result = valueRegistry.TryResolveValue(value, out IGraphValue? graphValue);

            // Assert
            result.Should().BeTrue();
            graphValue.Should().NotBeNull().And.BeOfType<GraphListValue>();
            graphValue.As<GraphListValue>().Value.Should()
                .HaveCount(1).And
                .AllSatisfy(v => v.Should().BeOfType<GraphNullValue>());
        }

        [Fact]
        public void TryResolveValue_ReturnsObjectValue_GivenDictionary()
        {
            // Arrange
            object? value = new Dictionary<string, string> { { "key", "value" } };
            ValueRegistry valueRegistry = new ValueRegistry();

            // Act
            bool result = valueRegistry.TryResolveValue(value, out IGraphValue? graphValue);

            // Assert
            result.Should().BeTrue();
            graphValue.Should().NotBeNull().And.BeOfType<GraphObjectValue>();
            graphValue.As<GraphObjectValue>().Value.Should().NotBeNull();
            graphValue.As<GraphObjectValue>().Value.Should().SatisfyRespectively(
                value =>
                {
                    value.Key.Value.Should().Be("key");
                    value.Value.Should().BeOfType<GraphStringValue>();
                    value.Value.As<GraphStringValue>().Value.Should().Be("value");
                }
            );
        }

        [Fact]
        public void TryResolveValue_ThrowsException_GivenDictionaryWithNonStringKey()
        {
            // Arrange
            object? value = new Dictionary<int, string> { { 1, "value" } };
            ValueRegistry valueRegistry = new ValueRegistry();

            // Act
            Action act = () => valueRegistry.TryResolveValue(value, out IGraphValue? graphValue);

            // Assert
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void TryResolveValue_ReturnsObjectValue_GivenDictionaryWithNullValue()
        {
            // Arrange
            object? value = new Dictionary<string, object?> { { "key", null } };
            ValueRegistry valueRegistry = new ValueRegistry();

            // Act
            bool result = valueRegistry.TryResolveValue(value, out IGraphValue? graphValue);

            // Assert
            result.Should().BeTrue();
            graphValue.Should().NotBeNull().And.BeOfType<GraphObjectValue>();
            graphValue.As<GraphObjectValue>().Value.Should().NotBeNull();
            graphValue.As<GraphObjectValue>().Value.Should().SatisfyRespectively(
                value =>
                {
                    value.Key.Value.Should().Be("key");
                    value.Value.Should().BeOfType<GraphNullValue>();
                    value.Value.As<GraphNullValue>().Value.Should().BeNull();
                }
            );
        }

        public static IEnumerable<object?[]> ValueBindingTypes =>
            new List<object?[]>
            {
                new object?[] { "", typeof(GraphStringValue) },
                new object?[] { "string", typeof(GraphStringValue) },
                new object?[] { true, typeof(GraphBooleanValue) },
                new object?[] { false, typeof(GraphBooleanValue) },
                new object?[] { null, typeof(GraphNullValue) },
                new object?[] { SByte.MaxValue, typeof(GraphIntValue) },
                new object?[] { Int16.MaxValue, typeof(GraphIntValue) },
                new object?[] { Int32.MaxValue, typeof(GraphIntValue) },
                new object?[] { Byte.MaxValue, typeof(GraphIntValue) },
                new object?[] { UInt16.MaxValue, typeof(GraphIntValue) },
                new object?[] { 2.53F, typeof(GraphFloatValue) },
                new object?[] { 2.53D, typeof(GraphFloatValue) },
            };
    }
}
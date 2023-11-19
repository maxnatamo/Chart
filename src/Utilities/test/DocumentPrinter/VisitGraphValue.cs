namespace Chart.Utilities.Tests.DocumentPrinterTests
{
    public class VisitGraphValueTests
    {
        [Fact]
        public void PrintsVariableField()
        {
            new DocumentPrinter()
                .Visit(new GraphVariableValue()
                {
                    Value = new GraphName("var")
                })
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void PrintsIntegerField()
        {
            new DocumentPrinter()
                .Visit(new GraphIntValue()
                {
                    Value = 123
                })
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void PrintsFloatField()
        {
            new DocumentPrinter()
                .Visit(new GraphFloatValue()
                {
                    Value = 123.456f
                })
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void PrintsStringField()
        {
            new DocumentPrinter()
                .Visit(new GraphStringValue()
                {
                    Value = "Some text"
                })
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void PrintsBooleanField()
        {
            new DocumentPrinter()
                .Visit(new GraphBooleanValue()
                {
                    Value = true
                })
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void PrintsNullField()
        {
            new DocumentPrinter()
                .Visit(new GraphNullValue())
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void PrintsEnumField()
        {
            new DocumentPrinter()
                .Visit(new GraphEnumValue()
                {
                    Value = new GraphName("PERSON")
                })
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void PrintsListField()
        {
            new DocumentPrinter()
                .Visit(new GraphListValue(new List<IGraphValue>()
                {
                    new GraphStringValue("Value1"),
                    new GraphStringValue("Value2"),
                    new GraphStringValue("Value3"),
                }))
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void PrintsObjectField()
        {
            new DocumentPrinter()
                .Visit(new GraphObjectValue(new Dictionary<GraphName, IGraphValue>()
                {
                    { new GraphName("a"), new GraphIntValue(1) },
                    { new GraphName("b"), new GraphIntValue(2) },
                    { new GraphName("c"), new GraphIntValue(3) }
                }))
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void ThrowsOnInvalidValue()
        {
            // Arrange
            // Act
            Action act = () => new DocumentPrinter().Visit(new InvalidGraphValue());

            // Assert
            act.Should().ThrowExactly<NotSupportedException>();
        }

        private class InvalidGraphValue : IGraphValue
        {
            public GraphValueKind ValueKind => (GraphValueKind) 10000;

            /// <inheritdoc />
            public string? Value { get; } = null;

            /// <inheritdoc />
            object? IGraphValue.Value => this.Value;

            /// <inheritdoc />
            public GraphLocation? Location { get; set; } = null;

            /// <inheritdoc />
            public override string ToString() => string.Empty;
        }
    }
}
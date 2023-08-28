namespace Chart.Language.SyntaxTree.Tests.SchemaWriterTests
{
    public class VisitGraphArgumentsTests
    {
        [Fact]
        public void Visit_PrintsArguments_GivenEmptyArguments()
        {
            // Arrange
            GraphArguments arguments = new GraphArguments();

            // Act

            // Assert
            new SchemaWriter()
                .Visit(arguments)
                .ToString()
                .Should().BeEmpty();
        }

        [Fact]
        public void Visit_PrintsArguments_GivenSingleArgument()
        {
            // Arrange
            GraphArguments arguments = new GraphArguments();
            arguments.Arguments.Add(new GraphArgument
            {
                Name = new GraphName("name"),
                Value = new GraphStringValue("Max")
            });

            // Act

            // Assert
            new SchemaWriter()
                .Visit(arguments)
                .ToString()
                .MatchSnapshot();
        }

        [Fact]
        public void Visit_PrintsArguments_GivenMultipleArguments()
        {
            // Arrange
            GraphArguments arguments = new GraphArguments();
            arguments.Arguments.Add(new GraphArgument
            {
                Name = new GraphName("name"),
                Value = new GraphStringValue("Max")
            });
            arguments.Arguments.Add(new GraphArgument
            {
                Name = new GraphName("age"),
                Value = new GraphIntValue(23)
            });

            // Act

            // Assert
            new SchemaWriter()
                .Visit(arguments)
                .ToString()
                .MatchSnapshot();
        }
    }
}
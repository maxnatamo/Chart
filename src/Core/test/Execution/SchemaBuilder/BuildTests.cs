using Chart.Language.SyntaxTree;

namespace Chart.Core.Tests.Execution.SchemaBuilderTests
{
    public class BuildTests
    {
        [Fact]
        public void Build_CreatesTypeDefinition_GivenSingleType()
        {
            // Arrange
            // Act
            Schema schema = Schema.From(@"
                type Book {
                    name: String!
                }",
                _ => _
                    .AddType<Query>()
                    .AddType<Book>());

            // Assert
            GraphDefinition? type = schema.Definitions.FirstOrDefault(v => v.Name == "Book");
            type.Should().NotBeNull();
            type.Should().BeOfType<GraphObjectType>();
            type.As<GraphObjectType>().Name.Should().Be("Book");
            type.As<GraphObjectType>().Fields.Should().NotBeNull();
            type.As<GraphObjectType>().Fields?.Fields.First().Name.Should().Be("name");
            type.As<GraphObjectType>().Fields?.Fields.First().Type.ToString().Should().Be("String!");
        }

        private class Query { }

        private class Book
        {
            public string Name => "The Raven";
        }
    }
}
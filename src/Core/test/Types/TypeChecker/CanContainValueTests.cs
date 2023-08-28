using Chart.Language;
using Chart.Language.SyntaxTree;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core.Tests.Types.TypeCheckerTests
{
    public class CanContainValueTests
    {
        [Fact]
        public void CanContainValue_ReturnsTrue_GivenObjectTypeWithNonNullTypeAndStringValue()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddSchema(@"
                type Book {
                    name: String!
                }")
                .AddType<StringQuery>("Query")
                .AddType<Book>()
                .BuildServiceProvider();

            ITypeChecker typeChecker = services.GetRequiredService<ITypeChecker>();

            Schema schema = services.GetRequiredService<SchemaAccessor>().Schema;
            QueryRequest query = new QueryRequestBuilder().Create();

            QueryExecutionContext context = new QueryExecutionContext(schema, query);

            // Act
            bool result = typeChecker.CanContainValue(
                new GraphNamedType("Book"),
                new GraphObjectValue(new Dictionary<GraphName, IGraphValue>
                {
                    { new GraphName("name"), new GraphStringValue("The Raven") }
                }),
                context
            );

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CanContainValue_ReturnsFalse_GivenObjectTypeWithNonNullTypeAndNullValue()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddSchema(@"
                type Book {
                    name: String!
                }")
                .AddType<StringQuery>("Query")
                .AddType<Book>()
                .BuildServiceProvider();

            ITypeChecker typeChecker = services.GetRequiredService<ITypeChecker>();

            Schema schema = services.GetRequiredService<SchemaAccessor>().Schema;
            QueryRequest query = new QueryRequestBuilder().Create();

            QueryExecutionContext context = new QueryExecutionContext(schema, query);

            // Act
            bool result = typeChecker.CanContainValue(
                new GraphNamedType("Book"),
                new GraphObjectValue(new Dictionary<GraphName, IGraphValue>
                {
                    { new GraphName("name"), new GraphNullValue() }
                }),
                context
            );

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CanContainValue_ReturnsTrue_GivenObjectTypeWithNullableTypeAndStringValue()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddSchema(@"
                type Book {
                    name: String!
                }")
                .AddType<StringQuery>("Query")
                .AddType<Book>()
                .BuildServiceProvider();

            ITypeChecker typeChecker = services.GetRequiredService<ITypeChecker>();

            Schema schema = services.GetRequiredService<SchemaAccessor>().Schema;
            QueryRequest query = new QueryRequestBuilder().Create();

            QueryExecutionContext context = new QueryExecutionContext(schema, query);

            // Act
            bool result = typeChecker.CanContainValue(
                new GraphNamedType("Book"),
                new GraphObjectValue(new Dictionary<GraphName, IGraphValue>
                {
                    { new GraphName("name"), new GraphStringValue("The Nevermore") }
                }),
                context
            );

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CanContainValue_ReturnsTrue_GivenObjectTypeWithNullableTypeAndNullValue()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddSchema(@"
                type Book {
                    name: String
                }")
                .AddType<StringQuery>("Query")
                .AddType<Book>()
                .BuildServiceProvider();

            ITypeChecker typeChecker = services.GetRequiredService<ITypeChecker>();

            Schema schema = services.GetRequiredService<SchemaAccessor>().Schema;
            QueryRequest query = new QueryRequestBuilder().Create();

            QueryExecutionContext context = new QueryExecutionContext(schema, query);

            // Act
            bool result = typeChecker.CanContainValue(
                new GraphNamedType("Book"),
                new GraphObjectValue(new Dictionary<GraphName, IGraphValue>
                {
                    { new GraphName("name"), new GraphNullValue() }
                }),
                context
            );

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CanContainValue_ReturnsTrue_GivenNestedObjectType()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddSchema(@"
                type Author {
                    name: String!
                }

                type Book {
                    author: Author!
                }")
                .AddType<StringQuery>("Query")
                .AddType<Author>()
                .AddType<Book>()
                .BuildServiceProvider();

            ITypeChecker typeChecker = services.GetRequiredService<ITypeChecker>();

            Schema schema = services.GetRequiredService<SchemaAccessor>().Schema;
            QueryRequest query = new QueryRequestBuilder().Create();

            QueryExecutionContext context = new QueryExecutionContext(schema, query);

            // Act
            bool result = typeChecker.CanContainValue(
                new GraphNamedType("Book"),
                new GraphObjectValue(new Dictionary<GraphName, IGraphValue>
                {
                    {
                        new GraphName("author"),
                        new GraphObjectValue(new Dictionary<GraphName, IGraphValue>
                        {
                            { new GraphName("name"), new GraphStringValue("Edgar Allan Poe") }
                        })
                    }
                }),
                context
            );

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CanContainValue_ReturnsFalse_GivenNestedObjectTypeWithDifferentValue()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddSchema(@"
                type Author {
                    name: String!
                }

                type Book {
                    author: Author!
                }")
                .AddType<StringQuery>("Query")
                .AddType<Author>()
                .AddType<Book>()
                .BuildServiceProvider();

            ITypeChecker typeChecker = services.GetRequiredService<ITypeChecker>();

            Schema schema = services.GetRequiredService<SchemaAccessor>().Schema;
            QueryRequest query = new QueryRequestBuilder().Create();

            QueryExecutionContext context = new QueryExecutionContext(schema, query);

            // Act
            bool result = typeChecker.CanContainValue(
                new GraphNamedType("Book"),
                new GraphObjectValue(new Dictionary<GraphName, IGraphValue>
                {
                    {
                        new GraphName("author"),
                        new GraphObjectValue(new Dictionary<GraphName, IGraphValue>
                        {
                            { new GraphName("name"), new GraphIntValue(10) }
                        })
                    }
                }),
                context
            );

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CanContainValue_ReturnsTrue_GivenListTypeWithListValue()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddSchema(@"
                type Classroom {
                    pupils: [String!]!
                }")
                .AddType<StringQuery>("Query")
                .AddType<Classroom>()
                .BuildServiceProvider();

            ITypeChecker typeChecker = services.GetRequiredService<ITypeChecker>();

            Schema schema = services.GetRequiredService<SchemaAccessor>().Schema;
            QueryRequest query = new QueryRequestBuilder().Create();

            QueryExecutionContext context = new QueryExecutionContext(schema, query);

            // Act
            bool result = typeChecker.CanContainValue(
                new GraphNamedType("Classroom"),
                new GraphObjectValue(new Dictionary<GraphName, IGraphValue>
                {
                    {
                        new GraphName("pupils"),
                        new GraphListValue(new List<IGraphValue>
                        {
                            new GraphStringValue("Adam Smasher"),
                            new GraphStringValue("Jason Hall"),
                        })
                    }
                }),
                context
            );

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CanContainValue_ReturnsFalse_GivenListTypeWithNonListValue()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddSchema(@"
                type Classroom {
                    pupils: [String!]!
                }")
                .AddType<StringQuery>("Query")
                .AddType<Classroom>()
                .BuildServiceProvider();

            ITypeChecker typeChecker = services.GetRequiredService<ITypeChecker>();

            Schema schema = services.GetRequiredService<SchemaAccessor>().Schema;
            QueryRequest query = new QueryRequestBuilder().Create();

            QueryExecutionContext context = new QueryExecutionContext(schema, query);

            // Act
            bool result = typeChecker.CanContainValue(
                new GraphNamedType("Classroom"),
                new GraphObjectValue(new Dictionary<GraphName, IGraphValue>
                {
                    {
                        new GraphName("pupils"),
                        new GraphStringValue("Johnny Test")
                    }
                }),
                context
            );

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CanContainValue_ReturnsFalse_GivenStringTypeWithListValue()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddSchema(@"
                type Classroom {
                    pupils: [String!]!
                    teacher: String!
                }")
                .AddType<StringQuery>("Query")
                .AddType<Classroom>()
                .BuildServiceProvider();

            ITypeChecker typeChecker = services.GetRequiredService<ITypeChecker>();

            Schema schema = services.GetRequiredService<SchemaAccessor>().Schema;
            QueryRequest query = new QueryRequestBuilder().Create();

            QueryExecutionContext context = new QueryExecutionContext(schema, query);

            // Act
            bool result = typeChecker.CanContainValue(
                new GraphNamedType("Classroom"),
                new GraphObjectValue(new Dictionary<GraphName, IGraphValue>
                {
                    {
                        new GraphName("teacher"),
                        new GraphListValue(new List<IGraphValue>
                        {
                            new GraphStringValue("Adam Smasher"),
                            new GraphStringValue("Jason Hall"),
                        })
                    }
                }),
                context
            );

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CanContainValue_ReturnsTrue_GivenCaseSensitiveEnumValue()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddSchema(@"
                enum Direction { NORTH, EAST, SOUTH, WEST }

                type Compass {
                    direction: Direction!
                }")
                .AddType<StringQuery>("Query")
                .AddType<Direction>()
                .AddType<Compass>()
                .BuildServiceProvider();

            ITypeChecker typeChecker = services.GetRequiredService<ITypeChecker>();

            Schema schema = services.GetRequiredService<SchemaAccessor>().Schema;
            QueryRequest query = new QueryRequestBuilder().Create();

            QueryExecutionContext context = new QueryExecutionContext(schema, query);

            // Act
            bool result = typeChecker.CanContainValue(
                new GraphNamedType("Compass"),
                new GraphObjectValue(new Dictionary<GraphName, IGraphValue>
                {
                    {
                        new GraphName("direction"),
                        new GraphEnumValue("SOUTH")
                    }
                }),
                context
            );

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CanContainValue_ReturnsFalse_GivenCaseInsensitiveEnumValue()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddSchema(@"
                enum Direction { NORTH, EAST, SOUTH, WEST }

                type Compass {
                    direction: Direction!
                }")
                .AddType<StringQuery>("Query")
                .AddType<Direction>()
                .AddType<Compass>()
                .BuildServiceProvider();

            ITypeChecker typeChecker = services.GetRequiredService<ITypeChecker>();

            Schema schema = services.GetRequiredService<SchemaAccessor>().Schema;
            QueryRequest query = new QueryRequestBuilder().Create();

            QueryExecutionContext context = new QueryExecutionContext(schema, query);

            // Act
            bool result = typeChecker.CanContainValue(
                new GraphNamedType("Compass"),
                new GraphObjectValue(new Dictionary<GraphName, IGraphValue>
                {
                    {
                        new GraphName("direction"),
                        new GraphEnumValue("South")
                    }
                }),
                context
            );

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CanContainValue_ReturnsFalse_GivenEnumTypeAndStringValue()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddSchema(@"
                enum Direction { NORTH, EAST, SOUTH, WEST }

                type Compass {
                    direction: Direction!
                }")
                .AddType<StringQuery>("Query")
                .AddType<Direction>()
                .AddType<Compass>()
                .BuildServiceProvider();

            ITypeChecker typeChecker = services.GetRequiredService<ITypeChecker>();

            Schema schema = services.GetRequiredService<SchemaAccessor>().Schema;
            QueryRequest query = new QueryRequestBuilder().Create();

            QueryExecutionContext context = new QueryExecutionContext(schema, query);

            // Act
            bool result = typeChecker.CanContainValue(
                new GraphNamedType("Compass"),
                new GraphObjectValue(new Dictionary<GraphName, IGraphValue>
                {
                    {
                        new GraphName("direction"),
                        new GraphStringValue("SOUTH")
                    }
                }),
                context
            );

            // Assert
            result.Should().BeFalse();
        }
    }
}
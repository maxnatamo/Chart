using Chart.Testing.Extensions;

namespace Chart.Core.Tests.Execution.SchemaBuilderTests
{
    public class BuildTests
    {
        [Fact]
        public void Build_AddsType_GivenType()
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
            schema.Should()
                .ContainType<Book>("it was explicitly added").And
                .ContainType<Query>("it was explicitly added");
        }

        [Fact]
        public void Build_AddsDerivedType_GivenType()
        {
            // Arrange
            // Act
            Schema schema = Schema.Create(_ => _.AddType<Query>());

            // Assert
            schema.Should()
                .ContainType<Book>("it was contained within Query").And
                .ContainType<Query>("it was explicitly added");
        }

        [Fact]
        public void Build_AddsAliasedType_GivenTypeWithAlias()
        {
            // Arrange
            // Act
            Schema schema = Schema.Create(_ => _.AddType<Book>("Query"));

            // Assert
            schema.Should()
                .ContainType("Query", "it was aliased to Query").And
                .NotContainType<Book>("it was aliased to Query");
        }

        [Fact]
        public void Build_AddsEnumType_GivenType()
        {
            // Arrange
            // Act
            Schema schema = Schema.Create(_ => _
                .AddType<Query>("Query")
                .AddType<Compass>());

            // Assert
            schema.Should().ContainType<Compass>();
        }

        [Fact]
        public void Build_AddsScalarType_GivenType()
        {
            // Arrange
            // Act
            Schema schema = Schema.Create(_ => _
                .AddType<Query>("Query")
                .AddType<TestScalar>());

            // Assert
            schema.Should().ContainType<TestScalar>();
        }

        [Fact]
        public void Build_AddsDirectiveType_GivenType()
        {
            // Arrange
            // Act
            Schema schema = Schema.Create(_ => _
                .AddType<Query>("Query")
                .AddType<TestDirective>());

            // Assert
            schema.Should().ContainType<TestDirective>();
        }

        [Fact]
        public void Build_ThrowsException_GivenValueType()
        {
            // Arrange
            // Act
            Action act = () => Schema.Create(_ => _
                .AddType<Query>("Query")
                .AddType(typeof(int)));

            // Assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void Build_ThrowsException_GivenDuplicateTypeName()
        {
            // Arrange
            // Act
            Action act = () => Schema.Create(_ => _
                .AddType<Query>()
                .AddType<Book>("Query"));

            // Assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        private class Query
        {
            public Book GetBook => new();
        }

        private class Book
        {
            public string Name => "The Raven";
        }
    }
}
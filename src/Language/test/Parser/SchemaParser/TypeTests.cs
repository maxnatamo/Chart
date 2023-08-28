namespace Chart.Language.Tests
{
    public class TypeTests
    {
        [Fact]
        public void ObjectTypeWithoutFieldsReturnsTypeWithoutFields()
        {
            // Arrange
            string source = "type Test";

            // Act
            GraphDocument document = new SchemaParser().ParseSchema(source);

            // Assert
            document.Should().NotBeNull();
            document.Definitions.Should().NotBeEmpty();
            document.Definitions[0].DefinitionKind.Should().Be(GraphDefinitionKind.Type);

            document.Definitions[0]
                .As<GraphObjectType>().TypeKind
                .Should().Be(GraphTypeDefinitionKind.Object);

            document.Definitions[0]
                .As<GraphObjectType>().Name.Should().NotBeNull();

            document.Definitions[0]
                .As<GraphObjectType>().Name?.Value.Should().Be("Test");

            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields.Should().BeEmpty();
        }

        [Fact]
        public void EmptyObjectTypeReturnsTypeWithoutFields()
        {
            // Arrange
            string source = "type Test { }";

            // Act
            GraphDocument document = new SchemaParser().ParseSchema(source);

            // Assert
            document.Should().NotBeNull();
            document.Definitions.Should().NotBeEmpty();
            document.Definitions[0].DefinitionKind.Should().Be(GraphDefinitionKind.Type);

            document.Definitions[0]
                .As<GraphObjectType>().TypeKind
                .Should().Be(GraphTypeDefinitionKind.Object);

            document.Definitions[0]
                .As<GraphObjectType>().Name.Should().NotBeNull();

            document.Definitions[0]
                .As<GraphObjectType>().Name?.Value.Should().Be("Test");

            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields.Should().BeEmpty();
        }

        [Fact]
        public void StringFieldReturnsNullableString()
        {
            // Arrange
            string source = "type Test { field: String }";

            // Act
            GraphDocument document = new SchemaParser().ParseSchema(source);

            // Assert
            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields[0]
                .As<GraphField>().Name.Value.Should().Be("field");

            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields[0]
                .As<GraphField>().Type.TypeKind.Should().Be(GraphTypeKind.Named);

            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields[0]
                .As<GraphField>().Type
                .As<GraphNamedType>().Name.Value.Should().Be("String");

            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields[0]
                .As<GraphField>().Type.NonNullable.Should().BeFalse();
        }

        [Fact]
        public void NonNullableStringFieldReturnsNonNullableString()
        {
            // Arrange
            string source = "type Test { field: String! }";

            // Act
            GraphDocument document = new SchemaParser().ParseSchema(source);

            // Assert
            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields[0]
                .As<GraphField>().Name.Value.Should().Be("field");

            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields[0]
                .As<GraphField>().Type.TypeKind.Should().Be(GraphTypeKind.Named);

            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields[0]
                .As<GraphField>().Type
                .As<GraphNamedType>().Name.Value.Should().Be("String");

            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields[0]
                .As<GraphField>().Type.NonNullable.Should().BeTrue();
        }

        [Fact]
        public void StringListFieldReturnsNullableStringList()
        {
            // Arrange
            string source = "type Test { field: [String] }";

            // Act
            GraphDocument document = new SchemaParser().ParseSchema(source);

            // Assert
            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields[0]
                .As<GraphField>().Name.Value.Should().Be("field");

            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields[0]
                .As<GraphField>().Type.TypeKind.Should().Be(GraphTypeKind.List);

            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields[0]
                .As<GraphField>().Type
                .As<GraphListType>().NonNullable.Should().BeFalse();

            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields[0]
                .As<GraphField>().Type
                .As<GraphListType>().UnderlyingType
                .As<GraphNamedType>().Name.Value.Should().Be("String");

            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields[0]
                .As<GraphField>().Type
                .As<GraphListType>().UnderlyingType
                .As<GraphNamedType>().NonNullable.Should().BeFalse();
        }

        [Fact]
        public void NestedListFieldReturnsNestedList()
        {
            // Arrange
            string source = "type Test { field: [[String]] }";

            // Act
            GraphDocument document = new SchemaParser().ParseSchema(source);

            // Assert
            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields[0]
                .As<GraphField>().Type.TypeKind.Should().Be(GraphTypeKind.List);

            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields[0]
                .As<GraphField>().Type
                .As<GraphListType>().NonNullable.Should().BeFalse();

            document.Definitions[0]
                .As<GraphObjectType>().Fields?.Fields[0]
                .As<GraphField>().Type
                .As<GraphListType>().UnderlyingType
                .As<GraphListType>().UnderlyingType
                .As<GraphNamedType>().Name.Value.Should().Be("String");
        }
    }
}
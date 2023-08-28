namespace Chart.Language.Tests
{
    public class ArgumentDefinitionTests
    {
        [Fact]
        public void ArgumentsDefinitionWithoutArgumentsReturnsEmptyGraphArgumentsDefinition()
        {
            // Arrange
            string source = @"type Person { picture(): Url }";

            // Act
            GraphDocument document = new SchemaParser().ParseSchema(source);

            // Assert
            document.Definitions[0]
                .As<GraphTypeDefinition>()
                .As<GraphObjectType>().Fields?.Fields[0].Arguments?.Arguments
                .Should().BeEmpty();
        }

        [Fact]
        public void ValidArgumentDefinitionReturnsGraphArgumentDefinition()
        {
            // Arrange
            string source = @"type Person { picture(size: Int): Url }";

            // Act
            GraphDocument document = new SchemaParser().ParseSchema(source);

            // Assert
            document.Definitions[0]
                .As<GraphTypeDefinition>()
                .As<GraphObjectType>().Fields?.Fields[0].Arguments?.Arguments[0]
                .Name.Value.Should().Be("size");

            document.Definitions[0]
                .As<GraphTypeDefinition>()
                .As<GraphObjectType>().Fields?.Fields[0].Arguments?.Arguments[0].Type
                .As<GraphNamedType>().Name.Value.Should().Be("Int");
        }

        [Fact]
        public void ArgumentDefinitionWithoutNameThrowsUnexpectedTokenException()
        {
            // Arrange
            string source = @"type Person { picture(: Int): Url }";

            // Act
            Action act = () => new SchemaParser().ParseSchema(source);

            // Assert
            act.Should().Throw<UnexpectedTokenException>();
        }

        [Fact]
        public void ArgumentDefinitionWithoutColonThrowsUnexpectedTokenException()
        {
            // Arrange
            string source = @"type Person { picture(size Int): Url }";

            // Act
            Action act = () => new SchemaParser().ParseSchema(source);

            // Assert
            act.Should().Throw<UnexpectedTokenException>();
        }

        [Fact]
        public void ArgumentDefinitionWithDefaultValueReturnsGraphArgumentDefinition()
        {
            // Arrange
            string source = @"type Person { picture(size: Int = 1): Url }";

            // Act
            GraphDocument document = new SchemaParser().ParseSchema(source);

            // Assert
            document.Definitions[0]
                .As<GraphTypeDefinition>()
                .As<GraphObjectType>().Fields?.Fields[0].Arguments?.Arguments[0]
                .DefaultValue.Should().NotBeNull();

            document.Definitions[0]
                .As<GraphTypeDefinition>()
                .As<GraphObjectType>().Fields?.Fields[0].Arguments?.Arguments[0]
                .DefaultValue?.ValueKind.Should().Be(GraphValueKind.Int);

            document.Definitions[0]
                .As<GraphTypeDefinition>()
                .As<GraphObjectType>().Fields?.Fields[0].Arguments?.Arguments[0]
                .DefaultValue?.As<GraphIntValue>().Value.Should().Be(1);
        }

        [Fact]
        public void ArgumentDefinitionWitDirectivesReturnsGraphArgumentDefinition()
        {
            // Arrange
            string source = @"type Person { picture(size: Int @directive): Url }";

            // Act
            GraphDocument document = new SchemaParser().ParseSchema(source);

            // Assert
            document.Definitions[0]
                .As<GraphTypeDefinition>()
                .As<GraphObjectType>().Fields?.Fields[0].Arguments?.Arguments[0]
                .Directives.Should().NotBeNull();

            document.Definitions[0]
                .As<GraphTypeDefinition>()
                .As<GraphObjectType>().Fields?.Fields[0].Arguments?.Arguments[0]
                .Directives?.Directives[0].Name.Value.Should().Be("directive");
        }
    }
}
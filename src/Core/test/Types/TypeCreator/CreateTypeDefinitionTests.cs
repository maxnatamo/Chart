using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core.Tests.Types.TypeCreatorTests
{
    public class CreateTypeDefinitionTests
    {
        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithField()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithField>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithProperty()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithProperty>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithMethod()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithMethod>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithExplicitName()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithExplicitName>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithDescription()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithDescription>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithDirective()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithDirective>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithDirectiveWithArguments()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithDirectiveWithArguments>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithNamedField()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithNamedField>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithNamedProperty()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithNamedProperty>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithNamedMethod()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithNamedMethod>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithMethodArgument()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithMethodArgument>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithMethodNullableArgument()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithMethodNullableArgument>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithMethodDefaultArgument()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithMethodDefaultArgument>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithMethodNullableDefaultArgument()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithMethodNullableDefaultArgument>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithNamedMethodArgument()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithNamedMethodArgument>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithMethodArgumentWithDescription()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithMethodArgumentWithDescription>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithVoidMethodReturnType()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithVoidMethodReturnType>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        [Fact]
        public void CreateTypeDefinition_Matches_GivenObjectWithGenericMethod()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeCreator typeCreator = services.GetRequiredService<ITypeCreator>();

            // Act
            ObjectType definition = (ObjectType) typeCreator.CreateTypeDefinition<ObjectWithGenericMethod>();

            // Assert
            definition.CreateSyntaxNode(services).MatchSnapshot();
        }

        internal class ObjectWithField
        {
            public string Field = "Value";
        }

        internal class ObjectWithProperty
        {
            public string Property { get; set; } = "Value";
        }

        internal class ObjectWithMethod
        {
            public string Method() => "Value";
        }

        [GraphName("ExplicitObjectName")]
        internal class ObjectWithExplicitName
        {
            public string Field = "Value";
        }

        [GraphDescription("Custom description")]
        internal class ObjectWithDescription
        {
            public string Field = "Value";
        }

        [GraphDirective("deprecated")]
        internal class ObjectWithDirective
        {
            public string Field = "Value";
        }

        [GraphDirective("deprecated", "because", "it sucks lol")]
        internal class ObjectWithDirectiveWithArguments
        {
            public string Field = "Value";
        }

        internal class ObjectWithNamedField
        {
            [GraphName("ExplicitField")]
            public string Field = "Value";
        }

        internal class ObjectWithNamedProperty
        {
            [GraphName("ExplicitField")]
            public string Property { get; set; } = "Value";
        }

        internal class ObjectWithNamedMethod
        {
            [GraphName("ExplicitField")]
            public string Field() => "Value";
        }

#pragma warning disable IDE0060
        internal class ObjectWithMethodArgument
        {
            public string Field(int argument) => "Value";
        }

        internal class ObjectWithMethodNullableArgument
        {
            public string Field(int? argument) => "Value";
        }

        internal class ObjectWithMethodDefaultArgument
        {
            public string Field(int argument = 123) => "Value";
        }

        internal class ObjectWithMethodNullableDefaultArgument
        {
            public string Field(int? argument = 4) => "Value";
        }

        internal class ObjectWithNamedMethodArgument
        {
            public string Field([GraphName("arg")] int argument) => "Value";
        }

        internal class ObjectWithMethodArgumentWithDescription
        {
            public string Field([GraphDescription("test argument")] int argument) => "Value";
        }

        internal class ObjectWithVoidMethodReturnType
        {
            public void Field() { }
        }

        internal class ObjectWithGenericMethod
        {
            public T Field<T>(T v) => v;
        }
#pragma warning restore IDE0060
    }
}
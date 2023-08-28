using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core.Tests.Types.TypeRegistratorTests
{
    public class RegisterTypeTests
    {
        [Theory]
        [InlineData(typeof(String))]
        [InlineData(typeof(Boolean))]
        [InlineData(typeof(Single))]
        [InlineData(typeof(Double))]
        [InlineData(typeof(Decimal))]
        [InlineData(typeof(SByte))]
        [InlineData(typeof(Int16))]
        [InlineData(typeof(Int32))]
        [InlineData(typeof(Int64))]
        public void TypeRegistry_ContainsType_GivenNativeScalars(Type scalarType)
        {
            // Arrange
            ITypeRegistry typeRegistry = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<ITypeRegistry>();

            // Act

            // Assert
            typeRegistry.Visited(scalarType).Should().BeTrue();
        }

        [Fact]
        public void Register_DoesntContainType_GivenUnregisteredType()
        {
            // Arrange
            ITypeRegistry typeRegistry = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<ITypeRegistry>();

            // Act

            // Assert
            typeRegistry.Visited<Book>().Should().BeFalse();
            typeRegistry.Visited<Author>().Should().BeFalse();
        }

        [Fact]
        public void Register_ContainsFieldTypes_GivenObjectType()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeRegistrator typeRegistrator = services.GetRequiredService<ITypeRegistrator>();
            ITypeRegistry typeRegistry = services.GetRequiredService<ITypeRegistry>();

            // Act
            typeRegistrator.Register<Book>();

            // Assert
            typeRegistry.Visited<Book>().Should().BeTrue();
            typeRegistry.Visited<Author>().Should().BeTrue();
        }

        [Fact]
        public void Register_ContainsPropertyTypes_GivenObjectType()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeRegistrator typeRegistrator = services.GetRequiredService<ITypeRegistrator>();
            ITypeRegistry typeRegistry = services.GetRequiredService<ITypeRegistry>();

            // Act
            typeRegistrator.Register<Author>();

            // Assert
            typeRegistry.Visited<Book>().Should().BeTrue();
            typeRegistry.Visited<Author>().Should().BeTrue();
        }

        [Fact]
        public void Register_ContainsGenericType_GivenGenericObjectType()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeRegistrator typeRegistrator = services.GetRequiredService<ITypeRegistrator>();
            ITypeRegistry typeRegistry = services.GetRequiredService<ITypeRegistry>();

            // Act
            typeRegistrator.Register<GenericClass<Author>>();

            // Assert
            typeRegistry.Visited<Book>().Should().BeTrue();
            typeRegistry.Visited<Author>().Should().BeTrue();
            typeRegistry.Visited<GenericClass<Author>>().Should().BeTrue();
        }

        [Fact]
        public void Register_ContainsGenericType_GivenObjectTypeWithGenericMethod()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeRegistrator typeRegistrator = services.GetRequiredService<ITypeRegistrator>();
            ITypeRegistry typeRegistry = services.GetRequiredService<ITypeRegistry>();

            // Act
            typeRegistrator.Register<GenericMethod>();

            // Assert
            typeRegistry.Visited<Book>().Should().BeFalse();
            typeRegistry.Visited<Author>().Should().BeFalse();
            typeRegistry.Visited<GenericMethod>().Should().BeTrue();
        }

        [Fact]
        public void Register_DoesntContainPropertyType_GivenObjectWithStaticProperty()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeRegistrator typeRegistrator = services.GetRequiredService<ITypeRegistrator>();
            ITypeRegistry typeRegistry = services.GetRequiredService<ITypeRegistry>();

            // Act
            typeRegistrator.Register<ClassWithStaticType>();

            // Assert
            typeRegistry.Visited<Book>().Should().BeFalse();
            typeRegistry.Visited<ClassWithStaticType>().Should().BeTrue();
        }

        [Fact]
        public void Register_ContainsInheritedTypes_GivenInheritedObjectType()
        {
            // Arrange
            IServiceProvider services = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider();

            ITypeRegistrator typeRegistrator = services.GetRequiredService<ITypeRegistrator>();
            ITypeRegistry typeRegistry = services.GetRequiredService<ITypeRegistry>();

            // Act
            typeRegistrator.Register<InheritedBook>();

            // Assert
            typeRegistry.Visited<Book>().Should().BeTrue();
            typeRegistry.Visited<Author>().Should().BeTrue();
            typeRegistry.Visited<InheritedBook>().Should().BeTrue();
        }
    }

    internal class Author
    {
        public string FirstName { get; set; } = "Edgar";

        public string LastName { get; set; } = "Allan Poe";

        public List<Book> Books { get; set; } = new List<Book>();
    }

    internal class Book
    {
        public string Title { get; set; } = "The Raven";

        public int ReleaseYear { get; set; } = 1845;

        public Author Author = new();
    }

    internal class InheritedBook : Book
    {
    }

    internal class GenericMethod
    {
        public TValue Method<TValue>() => default!;
    }

    internal class GenericClass<TValue>
    {

    }

    internal class ClassWithStaticType
    {
        public static Book Book { get; set; } = new();

        protected static Author Author { get; set; } = new();
    }
}
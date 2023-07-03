namespace Chart.Core.Tests.Types.TypeRegistryTests
{
    public class RegisterTypeTests
    {
        [Theory]
        [InlineData(typeof(Boolean))]
        [InlineData(typeof(Single))]
        [InlineData(typeof(Double))]
        [InlineData(typeof(Decimal))]
        [InlineData(typeof(SByte))]
        [InlineData(typeof(Int16))]
        [InlineData(typeof(Int32))]
        [InlineData(typeof(Int64))]
        [InlineData(typeof(Int128))]
        [InlineData(typeof(Byte))]
        [InlineData(typeof(UInt16))]
        [InlineData(typeof(UInt32))]
        [InlineData(typeof(UInt64))]
        [InlineData(typeof(UInt128))]
        [InlineData(typeof(String))]
        public void TypeRegistry_ContainsType_GivenNativeScalars(Type scalarType)
        {
            // Arrange
            TypeRegistry typeRegistry = new TypeRegistry();

            // Act

            // Assert
            typeRegistry.Visited(scalarType).Should().BeTrue();
        }

        [Fact]
        public void RegisterType_DoesntContainType_GivenUnregisteredType()
        {
            // Arrange
            TypeRegistry typeRegistry = new TypeRegistry();

            // Act

            // Assert
            typeRegistry.Visited<Book>().Should().BeFalse();
            typeRegistry.Visited<Author>().Should().BeFalse();
        }

        [Fact]
        public void RegisterType_ContainsFieldTypes_GivenObjectType()
        {
            // Arrange
            TypeRegistry typeRegistry = new TypeRegistry();

            // Act
            typeRegistry.Register<Book>();

            // Assert
            typeRegistry.Visited<Book>().Should().BeTrue();
            typeRegistry.Visited<Author>().Should().BeTrue();
        }

        [Fact]
        public void RegisterType_ContainsPropertyTypes_GivenObjectType()
        {
            // Arrange
            TypeRegistry typeRegistry = new TypeRegistry();

            // Act
            typeRegistry.Register<Author>();

            // Assert
            typeRegistry.Visited<Book>().Should().BeTrue();
            typeRegistry.Visited<Author>().Should().BeTrue();
        }

        [Fact]
        public void RegisterType_ContainsGenericType_GivenGenericObjectType()
        {
            // Arrange
            TypeRegistry typeRegistry = new TypeRegistry();

            // Act
            typeRegistry.Register<GenericClass<Author>>();

            // Assert
            typeRegistry.Visited<Book>().Should().BeTrue();
            typeRegistry.Visited<Author>().Should().BeTrue();
            typeRegistry.Visited<GenericClass<Author>>().Should().BeTrue();
        }

        [Fact]
        public void RegisterType_ContainsGenericType_GivenObjectTypeWithGenericMethod()
        {
            // Arrange
            TypeRegistry typeRegistry = new TypeRegistry();

            // Act
            typeRegistry.Register<GenericMethod>();

            // Assert
            typeRegistry.Visited<Book>().Should().BeFalse();
            typeRegistry.Visited<Author>().Should().BeFalse();
            typeRegistry.Visited<GenericMethod>().Should().BeTrue();
        }

        [Fact]
        public void RegisterType_DoesntContainPropertyType_GivenObjectWithStaticProperty()
        {
            // Arrange
            TypeRegistry typeRegistry = new TypeRegistry();

            // Act
            typeRegistry.Register<ClassWithStaticType>();

            // Assert
            typeRegistry.Visited<Book>().Should().BeFalse();
            typeRegistry.Visited<ClassWithStaticType>().Should().BeTrue();
        }

        [Fact]
        public void RegisterType_ContainsInheritedTypes_GivenInheritedObjectType()
        {
            // Arrange
            TypeRegistry typeRegistry = new TypeRegistry();

            // Act
            typeRegistry.Register<InheritedBook>();

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
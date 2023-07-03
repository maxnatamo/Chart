using Chart.Language.SyntaxTree;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core.Tests.Types.ObjectTypeDescriptorTests
{
    public class NameTests
    {
        [Fact]
        public void Name_UsesExplicitName_GivenObjectType()
        {
            // Arrange
            ServiceProvider provider = new ServiceCollection()
                .AddChart()
                .BuildServiceProvider();

            ObjectTypeDescriptor<Author> descriptor = new ObjectTypeDescriptor<Author>(
                provider.GetRequiredService<IObjectFieldDescriptorFactory>(),
                provider.GetRequiredService<INameFormatter>()
            );

            // Act
            descriptor.Name("bookAuthor");

            // Assert
            descriptor.objectName.Should().Be("bookAuthor");
        }

        [Fact]
        public void Name_ThrowsArgumentNullException_GivenNullName()
        {
            // Arrange
            ServiceProvider provider = new ServiceCollection()
                .AddChart()
                .BuildServiceProvider();

            ObjectTypeDescriptor<Author> descriptor = new ObjectTypeDescriptor<Author>(
                provider.GetRequiredService<IObjectFieldDescriptorFactory>(),
                provider.GetRequiredService<INameFormatter>()
            );

            // Act
            Action act = () => descriptor.Name(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Name_ThrowsArgumentNullException_GivenEmptyName()
        {
            // Arrange
            ServiceProvider provider = new ServiceCollection()
                .AddChart()
                .BuildServiceProvider();

            ObjectTypeDescriptor<Author> descriptor = new ObjectTypeDescriptor<Author>(
                provider.GetRequiredService<IObjectFieldDescriptorFactory>(),
                provider.GetRequiredService<INameFormatter>()
            );

            // Act
            Action act = () => descriptor.Name(string.Empty);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Name_UsesInferredName_GivenObjectType()
        {
            // Arrange
            ServiceProvider provider = new ServiceCollection()
                .AddChart()
                .BuildServiceProvider();

            ObjectTypeDescriptor<Author> descriptor = new ObjectTypeDescriptor<Author>(
                provider.GetRequiredService<IObjectFieldDescriptorFactory>(),
                provider.GetRequiredService<INameFormatter>()
            );

            // Act

            // Assert
            descriptor.objectName.Should().Be("author");
        }
    }

    internal class Author
    {
        public string FirstName { get; set; } = "Allan";

        public string LastName { get; set; } = "Poe";
    }
}
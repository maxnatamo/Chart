using Chart.Language.SyntaxTree;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core.Tests.Types.ObjectTypeDescriptorTests
{
    public class FieldTests
    {
        [Fact]
        public void Field_SelectsFieldWithSameName_GivenObjectType()
        {
            // Arrange
            ServiceProvider provider = new ServiceCollection()
                .AddChart()
                .BuildServiceProvider();

            ObjectTypeDescriptor<TestClass> descriptor = new ObjectTypeDescriptor<TestClass>(
                provider.GetRequiredService<IObjectFieldDescriptorFactory>(),
                provider.GetRequiredService<INameFormatter>()
            );

            // Act
            descriptor.Field("Field");

            // Assert
            descriptor._fields.Should().ContainSingle();
            descriptor._fields[0].fieldName.Should().Be("Field");
            descriptor._fields[0].description.Should().BeNull();
            descriptor._fields[0].schemaType.Should().NotBeNull();
            descriptor._fields[0].schemaType.Should().Be(typeof(GraphStringValue));
        }

        [Fact]
        public void Field_SelectsPropertyWithSameName_GivenObjectType()
        {
            // Arrange
            ServiceProvider provider = new ServiceCollection()
                .AddChart()
                .BuildServiceProvider();

            ObjectTypeDescriptor<TestClass> descriptor = new ObjectTypeDescriptor<TestClass>(
                provider.GetRequiredService<IObjectFieldDescriptorFactory>(),
                provider.GetRequiredService<INameFormatter>()
            );

            // Act
            descriptor.Field("Property");

            // Assert
            descriptor._fields.Should().ContainSingle();
            descriptor._fields[0].fieldName.Should().Be("Property");
            descriptor._fields[0].description.Should().BeNull();
            descriptor._fields[0].schemaType.Should().NotBeNull();
            descriptor._fields[0].schemaType.Should().Be(typeof(GraphStringValue));
        }

        [Fact]
        public void Field_SelectsMethodWithSameName_GivenObjectType()
        {
            // Arrange
            ServiceProvider provider = new ServiceCollection()
                .AddChart()
                .BuildServiceProvider();

            ObjectTypeDescriptor<TestClass> descriptor = new ObjectTypeDescriptor<TestClass>(
                provider.GetRequiredService<IObjectFieldDescriptorFactory>(),
                provider.GetRequiredService<INameFormatter>()
            );

            // Act
            descriptor.Field("Method");

            // Assert
            descriptor._fields.Should().ContainSingle();
            descriptor._fields[0].fieldName.Should().Be("Method");
            descriptor._fields[0].description.Should().BeNull();
            descriptor._fields[0].schemaType.Should().NotBeNull();
            descriptor._fields[0].schemaType.Should().Be(typeof(GraphStringValue));
        }

        [Fact]
        public void Field_InfersField_GivenObjectType()
        {
            // Arrange
            ServiceProvider provider = new ServiceCollection()
                .AddChart()
                .BuildServiceProvider();

            ObjectTypeDescriptor<TestClass> descriptor = new ObjectTypeDescriptor<TestClass>(
                provider.GetRequiredService<IObjectFieldDescriptorFactory>(),
                provider.GetRequiredService<INameFormatter>()
            );

            // Act
            descriptor.Field(v => v.Field);

            // Assert
            descriptor._fields.Should().ContainSingle();
            descriptor._fields[0].fieldName.Should().Be("field");
            descriptor._fields[0].description.Should().BeNull();
            descriptor._fields[0].schemaType.Should().NotBeNull();
            descriptor._fields[0].schemaType.Should().Be(typeof(GraphStringValue));
        }

        [Fact]
        public void Field_InfersProperty_GivenObjectType()
        {
            // Arrange
            ServiceProvider provider = new ServiceCollection()
                .AddChart()
                .BuildServiceProvider();

            ObjectTypeDescriptor<TestClass> descriptor = new ObjectTypeDescriptor<TestClass>(
                provider.GetRequiredService<IObjectFieldDescriptorFactory>(),
                provider.GetRequiredService<INameFormatter>()
            );

            // Act
            descriptor.Field(v => v.Property);

            // Assert
            descriptor._fields.Should().ContainSingle();
            descriptor._fields[0].fieldName.Should().Be("property");
            descriptor._fields[0].description.Should().BeNull();
            descriptor._fields[0].schemaType.Should().NotBeNull();
            descriptor._fields[0].schemaType.Should().Be(typeof(GraphStringValue));
        }

        [Fact]
        public void Field_InfersMethod_GivenObjectType()
        {
            // Arrange
            ServiceProvider provider = new ServiceCollection()
                .AddChart()
                .BuildServiceProvider();

            ObjectTypeDescriptor<TestClass> descriptor = new ObjectTypeDescriptor<TestClass>(
                provider.GetRequiredService<IObjectFieldDescriptorFactory>(),
                provider.GetRequiredService<INameFormatter>()
            );

            // Act
            descriptor.Field(v => v.Method());

            // Assert
            descriptor._fields.Should().ContainSingle();
            descriptor._fields[0].fieldName.Should().Be("method");
            descriptor._fields[0].description.Should().BeNull();
            descriptor._fields[0].schemaType.Should().NotBeNull();
            descriptor._fields[0].schemaType.Should().Be(typeof(GraphStringValue));
        }

        internal class TestClass
        {
            public string Field = "field";

            public string Property => "property";

            public string Method() => "method";
        }
    }
}
using Chart.Language.SyntaxTree;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core.Tests.Execution.ResolverCompilerTests
{
    public class CompileResolverTests
    {
        [Fact]
        public void CompileResolver_ThrowsException_GivenNonMemberExpression()
        {
            // Arrange
            IResolverCompiler compiler = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<IResolverCompiler>();

            // Act
            Action act = () => compiler.CompileResolver<TestClass>(v => "invalid value");

            // Assert
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void CompileResolver_CreatesFieldResolver_GivenField()
        {
            // Arrange
            IResolverCompiler compiler = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<IResolverCompiler>();

            ResolverContext context = this.CreateMockContext(new TestClass());

            FieldResolverDelegate resolver = compiler.CompileResolver<TestClass>(v => v.Field);

            // Act
            object? result = resolver(context);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<string>();
            result.As<string>().Should().Be("Field value");
        }

        [Fact]
        public void CompileResolver_CreatesFieldResolver_GivenFieldWithArguments()
        {
            // Arrange
            IResolverCompiler compiler = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<IResolverCompiler>();

            ResolverContext context = this.CreateMockContext(
                new TestClass(),
                new Dictionary<string, object?>
                {
                    { "arg", "sample value" }
                });

            FieldResolverDelegate resolver = compiler.CompileResolver<TestClass>(v => v.Field);

            // Act
            object? result = resolver(context);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<string>();
            result.As<string>().Should().Be("Field value");
        }

        [Fact]
        public void CompileResolver_CreatesPropertyResolver_GivenProperty()
        {
            // Arrange
            IResolverCompiler compiler = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<IResolverCompiler>();

            ResolverContext context = this.CreateMockContext(new TestClass());

            FieldResolverDelegate resolver = compiler.CompileResolver<TestClass>(v => v.Property);

            // Act
            object? result = resolver(context);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<string>();
            result.As<string>().Should().Be("Property value");
        }

        [Fact]
        public void CompileResolver_CreatesFieldResolver_GivenPropertyWithArguments()
        {
            // Arrange
            IResolverCompiler compiler = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<IResolverCompiler>();

            ResolverContext context = this.CreateMockContext(
                new TestClass(),
                new Dictionary<string, object?>
                {
                    { "arg", "sample value" }
                });

            FieldResolverDelegate resolver = compiler.CompileResolver<TestClass>(v => v.Property);

            // Act
            object? result = resolver(context);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<string>();
            result.As<string>().Should().Be("Property value");
        }

        [Fact]
        public void CompileResolver_CreatesMethodResolver_GivenParameterlessMethod()
        {
            // Arrange
            IResolverCompiler compiler = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<IResolverCompiler>();

            ResolverContext context = this.CreateMockContext(new TestClass());

            FieldResolverDelegate resolver = compiler.CompileResolver<TestClass>(v => v.ParameterlessMethod());

            // Act
            object? result = resolver(context);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<string>();
            result.As<string>().Should().Be("Parameterless value");
        }

        [Fact]
        public void CompileResolver_ThrowsException_GivenMethodWithParametersWithoutArguments()
        {
            // Arrange
            IResolverCompiler compiler = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<IResolverCompiler>();

            ResolverContext context = this.CreateMockContext(new TestClass());

            FieldResolverDelegate resolver = compiler.CompileResolver<TestClass>(v => v.LoopbackMethod(default));

            // Act
            Action act = () => resolver(context);

            // Assert
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void CompileResolver_CreatesMethodResolver_GivenMethodWithParameters()
        {
            // Arrange
            IResolverCompiler compiler = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<IResolverCompiler>();

            ResolverContext context = this.CreateMockContext(
                new TestClass(),
                new Dictionary<string, object?>
                {
                    { "value", "Loopback value" }
                });

            FieldResolverDelegate resolver = compiler.CompileResolver<TestClass>(v => v.LoopbackMethod(default));

            // Act
            object? result = resolver(context);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<string>();
            result.As<string>().Should().Be("Loopback value");
        }

        private ResolverContext CreateMockContext(
            object value,
            Dictionary<string, object?>? arguments = null,
            string name = "field"
        ) =>
            new ResolverContext(
                context: null!,
                objectType: null!,
                value,
                field: null!,
                selection: new GraphFieldSelection()
                {
                    Name = new GraphName(name ?? "field")
                },
                arguments,
                null!
            );

        private class TestClass
        {
            public string Field = "Field value";

            public string Property => "Property value";

            public string ParameterlessMethod() => "Parameterless value";

            public object? LoopbackMethod(object? value) => value;
        }
    }
}
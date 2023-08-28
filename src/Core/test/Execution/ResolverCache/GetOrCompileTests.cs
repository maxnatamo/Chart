using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core.Tests.Execution.ResolverCacheTests
{
    public class GetOrCompileTests
    {
        [Fact]
        public void GetOrCompile_ReturnsSameResolver_GivenSameMethod()
        {
            // Arrange
            IResolverCache cache = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<IResolverCache>();

            // Act
            FieldResolverDelegate resolver1 = cache.GetOrCompile(
                member: typeof(TestClass).GetMethods().Where(v => v.Name == nameof(TestClass.Get)).ToArray()[0],
                cache: true);

            FieldResolverDelegate resolver2 = cache.GetOrCompile(
                member: typeof(TestClass).GetMethods().Where(v => v.Name == nameof(TestClass.Get)).ToArray()[0],
                cache: true);

            // Assert
            resolver1.Should().BeSameAs(resolver2);
        }

        [Fact]
        public void GetOrCompile_ReturnsNewResolver_GivenMethodWithSameName()
        {
            // Arrange
            IResolverCache cache = new ServiceCollection()
                .AddChart()
                .AddType<StringQuery>("Query")
                .BuildServiceProvider()
                .GetRequiredService<IResolverCache>();

            // Act
            FieldResolverDelegate resolver1 = cache.GetOrCompile(
                member: typeof(TestClass).GetMethods().Where(v => v.Name == nameof(TestClass.Get)).ToArray()[0],
                cache: true);

            FieldResolverDelegate resolver2 = cache.GetOrCompile(
                member: typeof(TestClass).GetMethods().Where(v => v.Name == nameof(TestClass.Get)).ToArray()[1],
                cache: true);

            // Assert
            resolver1.Should().NotBeSameAs(resolver2);
        }

        private class TestClass
        {
            public string Get() => string.Empty;

            public string Get(string input) => string.Empty;
        }
    }
}
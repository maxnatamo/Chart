using Chart.Core;
using Chart.Testing.Assertions;

namespace Chart.Testing.Extensions
{
    public static class SchemExtensions
    {
        public static SchemaAssertions Should(this Schema instance)
            => new(instance);
    }
}
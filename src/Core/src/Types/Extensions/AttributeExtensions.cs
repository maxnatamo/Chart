using System.Reflection;

namespace Chart.Core
{
    public static partial class AttributeExtensions
    {
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this ICustomAttributeProvider provider)
            where TAttribute : Attribute =>
            provider
                .GetCustomAttributes(typeof(TAttribute), false)
                .Cast<TAttribute>();

        public static TAttribute? GetAttribute<TAttribute>(this ICustomAttributeProvider provider)
            where TAttribute : Attribute =>
            provider
                .GetAttributes<TAttribute>()
                .FirstOrDefault();
    }
}
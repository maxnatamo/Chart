namespace Chart.Core.Parsers
{
    public static class TypeExtensions
    {
        public static bool IsNullable(this Type type)
            => Nullable.GetUnderlyingType(type) != null;

        public static bool IsEnumerable(this Type type)
            => typeof(System.Collections.IList).IsAssignableFrom(type);
    }
}
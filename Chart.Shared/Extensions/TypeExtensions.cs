namespace Chart.Shared.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Determine whether a type an IEnumerable-object.
        /// </summary>
        /// <param name="type">The type to check for.</param>
        /// <returns>True, if the type is an IEnumerable. Otherwise, false.</returns>
        public static bool IsEnumerable(this Type type)
            => typeof(System.Collections.IEnumerable).IsAssignableFrom(type);
    }
}
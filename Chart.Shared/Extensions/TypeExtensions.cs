using System.Reflection;

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

        /// <summary>
        /// Get all properties of a type parameter.
        /// </summary>
        /// <param name="type">The type to retrieve properties from.</param>
        /// <returns>List of PropertyInfo-objects.</returns>
        public static List<PropertyInfo> GetLocalProperties(this Type type)
            => type.GetProperties(
                    BindingFlags.Instance |
                    BindingFlags.Public)
                .ToList();

        /// <summary>
        /// Get all fields of a type parameter.
        /// </summary>
        /// <param name="type">The type to retrieve properties from.</param>
        /// <returns>List of FieldInfo-objects.</returns>
        public static List<FieldInfo> GetLocalFields(this Type type)
            => type.GetFields(
                    BindingFlags.Instance |
                    BindingFlags.Public)
                .ToList();

        /// <summary>
        /// Get all methods of a type parameter.
        /// </summary>
        /// <remarks>
        /// Property getters/setters are not included.
        /// </remarks>
        /// <param name="type">The type to retrieve properties from.</param>
        /// <returns>List of MethodInfo-objects.</returns>
        public static List<MethodInfo> GetLocalMethods(this Type type)
            => type.GetMethods(
                    BindingFlags.DeclaredOnly |
                    BindingFlags.Instance |
                    BindingFlags.Public)
                .Where(m => !m.IsSpecialName)
                .ToList();
    }
}
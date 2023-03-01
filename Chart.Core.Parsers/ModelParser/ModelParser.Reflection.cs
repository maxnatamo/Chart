using System.Reflection;

namespace Chart.Core.Parsers
{
    public partial class ModelParser
    {
        /// <summary>
        /// Get all properties of a type parameter.
        /// </summary>
        /// <param name="type">The type to retrieve properties from.</param>
        /// <returns>List of PropertyInfo-objects.</returns>
        public List<PropertyInfo> GetProperties(Type type)
            => type.GetProperties(
                    BindingFlags.Instance |
                    BindingFlags.Public)
                .ToList();

        /// <summary>
        /// Get all fields of a type parameter.
        /// </summary>
        /// <param name="type">The type to retrieve properties from.</param>
        /// <returns>List of FieldInfo-objects.</returns>
        public List<FieldInfo> GetFields(Type type)
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
        public List<MethodInfo> GetMethods(Type type)
            => type.GetMethods(
                    BindingFlags.DeclaredOnly |
                    BindingFlags.Instance |
                    BindingFlags.Public)
                .Where(m => !m.IsSpecialName)
                .ToList();
    }
}
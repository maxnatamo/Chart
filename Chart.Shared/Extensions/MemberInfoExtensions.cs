using System.Reflection;

namespace Chart.Shared.Extensions
{
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Get all attributes of a specific type from a reflection type.
        /// </summary>
        /// <param name="info">The object to retrieve attributes from.</param>
        /// <typeparam name="T">The type of attribute to retrieve.</typeparam>
        /// <returns>List of the specified attribute types.</returns>
        public static List<T> GetAttributes<T>(this ICustomAttributeProvider info) where T : Attribute
        {
            return info.GetCustomAttributes(false)
                .Where(v => v is T)
                .Select(v => (T) v)
                .ToList();
        }

        /// <summary>
        /// Get an attribute of a specific type from a reflection type.
        /// </summary>
        /// <param name="info">The object to retrieve the attribute from.</param>
        /// <typeparam name="T">The type of attribute to retrieve.</typeparam>
        /// <returns>The attribute found, if any. Otherwise, null.</returns>
        public static T? GetAttribute<T>(this ICustomAttributeProvider info) where T : Attribute
        {
            var attrs = info.GetAttributes<T>();
            if(attrs.Any())
            {
                return attrs.First();
            }

            return null;
        }
    }
}
using System.Reflection;

namespace Chart.Core.Parsers
{
    public static class MemberInfoExtensions
    {
        public static List<T> GetAttributes<T>(this ICustomAttributeProvider info) where T : Attribute
        {
            return info.GetCustomAttributes(false)
                .Where(v => v is T)
                .Select(v => (T) v)
                .ToList();
        }

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
using System.Reflection;
using System.Runtime.CompilerServices;

using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public static partial class TypeExtensions
    {
        /// <summary>
        /// Check whether the given type is anonymous.
        /// </summary>
        public static bool IsAnonymous(this Type type)
        {
            if(type == null)
            {
                return false;
            }

            return type.IsGenericType
                   && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic
                   && type.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase)
                   && type.Name.Contains("AnonymousType")
                   && type.GetCustomAttributes(typeof(CompilerGeneratedAttribute)).Any();
        }

        /// <summary>
        /// Check whether the given type is derived from <see cref="ListType{T}" />.
        /// </summary>
        public static bool IsListType(this Type type)
            => type.IsAssignableTo(typeof(IListType));

        /// <summary>
        /// Check whether the given type is derived from <see cref="ListType{T}" />.
        /// </summary>
        public static bool IsNonNullType(this Type type)
            => type.IsAssignableTo(typeof(INonNullType));

        /// <summary>
        /// Check whether the given type is derived from the given generic type.
        /// </summary>
        public static bool IsOfGenericType(this Type type, Type targetType)
        {
            if(!type.IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition().IsAssignableTo(targetType);
        }

        /// <summary>
        /// Get the underlying nullable type, if the given type is nullable. Otherwise, returns itself.
        /// </summary>
        public static Type GetNullableType(this Type type)
            => type.GetNullableType(out bool _);

        /// <summary>
        /// Whether the given type is nullable.
        /// </summary>
        public static bool IsNullableType(this Type type)
        {
            type.GetNullableType(out bool res);
            return res;
        }

        /// <summary>
        /// Get the underlying nullable type, if the given type is nullable. Otherwise, returns itself.
        /// </summary>
        /// <param name="isNullable">Set to <see langword="true" />, if the given type was nullable.</param>
        public static Type GetNullableType(this Type type, out bool isNullable)
        {
            if(type.IsValueType)
            {
                Type? underlyingType = Nullable.GetUnderlyingType(type);
                if(underlyingType is not null)
                {
                    isNullable = true;
                    return underlyingType;
                }
            }

            if(type.IsAssignableTo(typeof(IOptional)))
            {
                isNullable = true;
                return type.GetGenericArguments().First();
            }

            isNullable = false;
            return type;
        }
    }
}
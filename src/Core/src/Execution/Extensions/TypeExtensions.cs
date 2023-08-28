namespace Chart.Core
{
    public static partial class TypeExtensions
    {
        public static bool IsTypeDefinition(this Type type)
            => type.IsAssignableTo(typeof(TypeDefinition));

        public static bool IsClass(this Type type)
            => type.IsClass && !type.IsAssignableTo(typeof(Delegate));
    }
}
using System.Linq.Expressions;
using System.Reflection;

namespace Chart.Core
{
    public static class ObjectFieldDescriptorExtensions
    {
        public static void FillWith(
            this ObjectFieldDescriptor descriptor,
            DescriptorContext context,
            MemberInfo memberInfo)
        {
            descriptor.schemaType = memberInfo switch
            {
                FieldInfo fieldInfo => fieldInfo.FieldType,
                MethodInfo methodInfo => methodInfo.ReturnType,
                PropertyInfo propertyInfo => propertyInfo.PropertyType,

                _ => throw new ArgumentException()
            };

            descriptor.schemaType = context.TypeRegistry.ResolveType(descriptor.schemaType);
            descriptor.resolver = ctx => ctx.Result<object>();
        }

        public static void FillWith(
            this ObjectFieldDescriptor descriptor,
            DescriptorContext context,
            INameFormatter nameFormatter,
            MemberExpression memberExpression)
        {
            string fieldName = nameFormatter.FormatName(memberExpression.Member.Name);
            descriptor.Name(fieldName);

            Type memberType = memberExpression.Member switch
            {
                FieldInfo fieldInfo => fieldInfo.FieldType,
                MethodInfo methodInfo => methodInfo.ReturnType,
                PropertyInfo propertyInfo => propertyInfo.PropertyType,

                _ => throw new ArgumentException()
            };

            if(context.TypeRegistry.TryResolveType(memberType, out Type? schemaType))
            {
                descriptor.schemaType = schemaType;
            }
        }

        public static void FillWith(
            this ObjectFieldDescriptor descriptor,
            DescriptorContext context,
            INameFormatter nameFormatter,
            MethodCallExpression methodCallExpression)
        {
            string fieldName = nameFormatter.FormatName(methodCallExpression.Method.Name);
            descriptor.Name(fieldName);

            Type memberType = methodCallExpression.Method switch
            {
                MethodInfo methodInfo => methodInfo.ReturnType,
                _ => throw new ArgumentException()
            };

            if(context.TypeRegistry.TryResolveType(memberType, out Type? schemaType))
            {
                descriptor.schemaType = schemaType;
            }
        }
    }
}
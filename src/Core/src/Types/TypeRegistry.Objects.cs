using System.Reflection;

namespace Chart.Core
{
    public sealed partial class TypeRegistry
    {
        private TypeRegistry RegisterObjectType(Type objectType)
        {
            // Properties
            PropertyInfo[] properties = objectType.GetProperties(
                BindingFlags.Instance |
                BindingFlags.Public
            );

            foreach(PropertyInfo property in properties)
            {
                this.RegisterProperty(property);
            }

            // Fields
            FieldInfo[] fields = objectType.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public
            );

            foreach(FieldInfo field in fields)
            {
                this.RegisterField(field);
            }

            // Methods
            MethodInfo[] methods = objectType.GetMethods(
                BindingFlags.Instance |
                BindingFlags.Public
            );

            foreach(MethodInfo method in methods)
            {
                // Ignore property getters/setters and operator overloads
                if(method.IsSpecialName)
                {
                    continue;
                }

                this.RegisterMethod(method);
            }

            return this;
        }
    }
}
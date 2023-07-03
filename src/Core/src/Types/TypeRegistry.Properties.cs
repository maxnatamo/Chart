using System.Reflection;

namespace Chart.Core
{
    public sealed partial class TypeRegistry
    {
        private TypeRegistry RegisterProperty(PropertyInfo property)
        {
            this.Register(property.PropertyType);
            return this;
        }
    }
}
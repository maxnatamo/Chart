using System.Reflection;

namespace Chart.Core
{
    public sealed partial class TypeRegistry
    {
        private TypeRegistry RegisterField(FieldInfo field)
        {
            this.Register(field.FieldType);
            return this;
        }
    }
}
using System.Reflection;

namespace Chart.Core.TypeResolver
{
    public partial class Resolver
    {
        /// <summary>
        /// Parse a field and recursively parse types.
        /// </summary>
        /// <param name="info">The FieldInfo-object to parse.</param>
        private void ParseObjectField(FieldInfo info)
            => this.RegisterType(info.FieldType);
    }
}
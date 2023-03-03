using System.Reflection;

namespace Chart.Core.TypeResolver
{
    public partial class Resolver
    {
        /// <summary>
        /// Parse a property and recursively parse types.
        /// </summary>
        /// <param name="info">The PropertyInfo-object to parse.</param>
        private void ParseObjectProperty(PropertyInfo info)
            => this.RegisterType(info.PropertyType);
    }
}
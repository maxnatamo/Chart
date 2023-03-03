using System.Reflection;

namespace Chart.Core.TypeResolver
{
    public partial class Resolver
    {
        /// <summary>
        /// Parse a method and recursively parse types.
        /// </summary>
        /// <param name="info">The MethodInfo-object to parse.</param>
        private void ParseObjectMethod(MethodInfo info)
        {
            this.RegisterType(info.ReturnType);

            for(int i = 0; i < info.GetParameters().Count(); i++)
            {
                this.RegisterType(info.GetParameters()[i].ParameterType);
            }
        }
    }
}
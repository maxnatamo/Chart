using System.Reflection;

namespace Chart.Core
{
    public sealed partial class TypeRegistry
    {
        private TypeRegistry RegisterMethod(MethodInfo method)
        {
            if(method.ReturnType != typeof(void))
            {
                this.Register(method.ReturnType);
            }

            foreach(Type genericType in method.GetGenericArguments())
            {
                return this.Register(genericType);
            }

            foreach(ParameterInfo parameter in method.GetParameters())
            {
                return this.Register(parameter.ParameterType);
            }

            return this;
        }
    }
}
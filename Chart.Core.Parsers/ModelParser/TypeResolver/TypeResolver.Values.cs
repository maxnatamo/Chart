using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class TypeResolver
    {
        public GraphValue ResolveValue(object value)
        {
            if(value == null)
            {
                return new GraphNullValue();
            }

            var resolver = this.GetTypeResolver(value.GetType());
            if(resolver != null)
            {
                return resolver.ParseLiteral(value);
            }

            return this.ResolveObjectValue(value);
        }

        public GraphObjectValue ResolveObjectValue(object value)
        {
            throw new NotImplementedException("ResolveObjectValue not implemented.");
        }
    }
}
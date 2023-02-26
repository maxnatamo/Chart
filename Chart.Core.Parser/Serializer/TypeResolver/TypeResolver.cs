namespace Chart.Core.Parser
{
    public partial class TypeResolver
    {
        private Dictionary<Type, GraphBaseType> DefinedTypes = new Dictionary<Type, GraphBaseType>();

        public TypeResolver()
        {
            this.RegisterType<String>(new StringScalarType());
            this.RegisterType<Int32>(new IntScalarType());
        }

        public TypeResolver RegisterType<TModel>(GraphBaseType parser)
            => this.RegisterType(typeof(TModel), parser);

        public TypeResolver RegisterType(Type model, GraphBaseType parser)
        {
            if(this.DefinedTypes.ContainsKey(model))
            {
                this.DefinedTypes.Remove(model);
            }

            this.DefinedTypes.Add(model, parser);
            return this;
        }

        public GraphType ResolveType(Type type)
        {
            if(type.IsEnumerable())
            {
                var listType = new GraphListType();

                // TODO: IsNullable doesn't work with reference types.
                // listType.NonNullable = !type.IsNullable();
                listType.UnderlyingType = this.ResolveType(type.GetGenericArguments().First());

                return listType;
            }
            
            GraphNamedType namedType = new GraphNamedType();
            namedType.NonNullable = true;

            if(type.IsNullable())
            {
                namedType.NonNullable = false;
                type = type.GetGenericArguments().First();
            }

            var resolver = this.GetTypeResolver(type);
            if(resolver == null)
            {
                namedType.Name = new GraphName(type.Name);
            }
            else
            {
                namedType.Name = new GraphName(resolver.Name);
            }
            
            return namedType;
        }

        private GraphBaseType? GetTypeResolver(Type type)
        {
            if(!this.DefinedTypes.ContainsKey(type))
            {
                return null;
            }
            return this.DefinedTypes[type];
        }
    }
}
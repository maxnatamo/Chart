using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class IdType : ScalarType
    {
        public override ILeafValueResolver Resolver { get; protected set; }

        public IdType()
            : base(name: "ID")
        {
            this.Description = "The ID scalar type represents a unique identifier, often used to refetch an object or as the key for a cache.";
            this.SpecifiedBy = "http://spec.graphql.org/October2021/#sec-ID";

            this.RuntimeType = typeof(Guid);
            this.Resolver = new IdTypeResolver();
        }

        public override bool IsOfType(IGraphValue value) =>
            value switch
            {
                GraphIntValue => true,
                GraphStringValue => true,
                _ => false
            };

        public override bool IsOfType(object? value) =>
            value switch
            {
                Guid => true,
                string => true,
                int => true,
                long => true,
                _ => false
            };
    }

    public class IdTypeResolver : ILeafValueResolver
    {
        public object? ResolveLiteral(IGraphValue value) =>
            value switch
            {
                GraphIntValue _value => _value.ToString(),
                GraphStringValue _value => _value.Value,

                _ => throw new NotSupportedException()
            };

        public IGraphValue ResolveValue(object? value) =>
            value switch
            {
                int _value => new GraphStringValue(_value.ToString()),
                long _value => new GraphStringValue(_value.ToString()),
                string _value => new GraphStringValue(_value),
                Guid _value => new GraphStringValue(_value.ToString()),

                _ => throw new NotSupportedException()
            };

        public IGraphValue CoerceInput(IGraphValue value) =>
            value switch
            {
                GraphStringValue _value => _value,
                GraphIntValue _value => new GraphStringValue(_value.Value.ToString()),

                _ => throw new NotSupportedException()
            };

        public object? CoerceResult(object? value) =>
            value switch
            {
                string _value => _value,
                int _value => _value.ToString(),
                long _value => _value.ToString(),
                Guid _value => _value.ToString(),

                _ => throw new NotSupportedException()
            };
    }
}
using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class StringType : ScalarType
    {
        public override ILeafValueResolver Resolver { get; protected set; }

        public StringType()
            : base(name: "String")
        {
            this.Description = "The String scalar type represents a sequence of Unicode characters.";
            this.SpecifiedBy = "http://spec.graphql.org/October2021/#sec-String";

            this.RuntimeType = typeof(String);
            this.Resolver = new StringTypeResolver();
        }

        public override bool IsOfType(IGraphValue value) =>
            value switch
            {
                GraphStringValue => true,
                _ => false
            };

        public override bool IsOfType(object? value)
            => value is string;
    }

    public class StringTypeResolver : ILeafValueResolver
    {
        public object? ResolveLiteral(IGraphValue value) =>
            value switch
            {
                GraphStringValue _value => _value.Value,

                _ => throw new NotSupportedException()
            };

        public IGraphValue ResolveValue(object? value) =>
            value switch
            {
                string _value => new GraphStringValue(_value),

                _ => throw new NotSupportedException()
            };

        public IGraphValue CoerceInput(IGraphValue value) =>
            value switch
            {
                GraphStringValue _value => _value,
                GraphIntValue _value => new GraphStringValue(_value.Value.ToString()),
                GraphFloatValue _value => new GraphStringValue(_value.Value.ToString()),
                GraphBooleanValue _value => new GraphStringValue(_value.Value.ToString()),
                GraphEnumValue _value => new GraphStringValue(_value.Value.ToString()),

                _ => throw new NotSupportedException()
            };

        public object? CoerceResult(object? value) =>
            value switch
            {
                string _value => _value,
                float _value => _value.ToString(),
                double _value => _value.ToString(),
                decimal _value => _value.ToString(),
                bool _value => _value.ToString(),
                byte _value => _value.ToString(),
                sbyte _value => _value.ToString(),
                short _value => _value.ToString(),
                ushort _value => _value.ToString(),
                int _value => _value.ToString(),
                uint _value => _value.ToString(),
                long _value => _value.ToString(),
                ulong _value => _value.ToString(),
                IntPtr _value => _value.ToString(),
                UIntPtr _value => _value.ToString(),

                _ => throw new NotSupportedException()
            };
    }
}
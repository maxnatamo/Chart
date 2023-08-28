using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class BooleanType : ScalarType
    {
        public override ILeafValueResolver Resolver { get; protected set; }

        public BooleanType()
            : base(name: "Boolean")
        {
            this.Description = "DateTime :)";
            this.SpecifiedBy = "me";

            this.RuntimeType = typeof(Boolean);
            this.Resolver = new BooleanTypeResolver();
        }

        public override bool IsOfType(IGraphValue value) =>
            value switch
            {
                GraphBooleanValue => true,
                GraphStringValue _value => _value.Value is "true" or "false",
                _ => false
            };

        public override bool IsOfType(object? value)
            => value is bool;
    }

    public class BooleanTypeResolver : ILeafValueResolver
    {
        public object? ResolveLiteral(IGraphValue value) =>
            value switch
            {
                GraphNullValue => null,
                GraphBooleanValue _value => _value.Value,

                _ => throw new NotSupportedException()
            };

        public IGraphValue ResolveValue(object? value) =>
            value switch
            {
                bool _value => new GraphBooleanValue(_value),
                string _value => new GraphBooleanValue(_value == "true"),

                _ => throw new NotSupportedException()
            };

        public IGraphValue CoerceInput(IGraphValue value) =>
            value switch
            {
                GraphBooleanValue _value => _value,
                GraphStringValue _value => new GraphBooleanValue(_value.Value == "true"),

                _ => throw new NotSupportedException($"Graph value of type {value.ValueKind} cannot be coerced into BooleanType.")
            };

        public object? CoerceResult(object? value) =>
            value switch
            {
                bool _value => _value,
                string _value => bool.Parse(_value),

                _ => throw new NotSupportedException($"Value of type '{value?.GetType().Name ?? "(null)"}' cannot be coerced into BooleanType.")
            };
    }
}
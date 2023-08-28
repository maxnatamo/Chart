using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class DateTimeType : ScalarType
    {
        public override ILeafValueResolver Resolver { get; protected set; }

        public DateTimeType()
            : base(name: "DateTime")
        {
            this.Description = "The DateTime scalar type represents an instant in time, date and time included.";

            this.RuntimeType = typeof(DateTime);
            this.Resolver = new DateTimeTypeResolver();
        }

        public override bool IsOfType(IGraphValue value)
            => value is GraphStringValue _value && DateTime.TryParse(_value.Value, out _);

        public override bool IsOfType(object? value)
            => value is DateTime;
    }

    public class DateTimeTypeResolver : ILeafValueResolver
    {
        public object? ResolveLiteral(IGraphValue value) =>
            value switch
            {
                GraphNullValue => null,
                GraphStringValue _value => DateTime.Parse(_value.Value),

                _ => throw new NotSupportedException()
            };

        public IGraphValue ResolveValue(object? value) =>
            value switch
            {
                DateTime _value => new GraphStringValue(_value.ToString()),
                string _value => this.ResolveValue(DateTime.Parse(_value)),

                _ => throw new NotSupportedException()
            };

        public IGraphValue CoerceInput(IGraphValue value) =>
            value switch
            {
                GraphStringValue _value => _value,

                _ => throw new NotSupportedException($"Graph value of type {value.ValueKind} cannot be coerced into DateTimeType.")
            };

        public object? CoerceResult(object? value) =>
            value switch
            {
                DateTime _value => _value,
                string _value => DateTime.Parse(_value),

                _ => throw new NotSupportedException($"Value of type '{value?.GetType().Name ?? "(null)"}' cannot be coerced into DateTimeType.")
            };
    }
}
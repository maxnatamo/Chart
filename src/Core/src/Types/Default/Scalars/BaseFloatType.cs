using System.Numerics;

using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public abstract class BaseFloatType<TFloatType> : ScalarType
        where TFloatType : IComparable
    {
        protected abstract TFloatType MinValue { get; }
        protected abstract TFloatType MaxValue { get; }

        public BaseFloatType(string name)
            : base(name)
        {
            this.RuntimeType = typeof(TFloatType);
        }

        public override bool IsOfType(IGraphValue value) =>
            value switch
            {
                GraphIntValue _value
                    => _value.Value.CompareTo(this.MinValue) >= 0 && _value.Value.CompareTo(this.MaxValue) <= 0,

                GraphFloatValue _value
                    => _value.Value.CompareTo(this.MinValue) >= 0 && _value.Value.CompareTo(this.MaxValue) <= 0,

                _ => false
            };

        public override bool IsOfType(object? value) =>
            value switch
            {
                TFloatType => true,
                float => true,
                double => true,
                decimal => true,

                SByte => true,
                Byte => true,
                Int16 => true,
                UInt16 => true,
                Int32 => true,
                UInt32 => true,
                Int64 => true,
                UInt64 => true,

                _ => false
            };
    }

    public abstract class BaseFloatTypeResolver<TFloatType> : ILeafValueResolver
        where TFloatType : IComparable
    {
        public object? ResolveLiteral(IGraphValue value) =>
            value switch
            {
                GraphNullValue => null,
                GraphIntValue _value => _value.Value,
                GraphFloatValue _value => _value.Value,

                _ => throw new NotSupportedException()
            };

        public IGraphValue ResolveValue(object? value) =>
            value switch
            {
                TFloatType _value => new GraphFloatValue(Convert.ToDouble(_value)),
                float _value => new GraphFloatValue(_value),
                double _value => new GraphFloatValue(_value),
                decimal _value => new GraphFloatValue(Convert.ToDouble(_value)),

                SByte _value => new GraphFloatValue(Convert.ToDouble(_value)),
                Byte _value => new GraphFloatValue(Convert.ToDouble(_value)),
                Int16 _value => new GraphFloatValue(Convert.ToDouble(_value)),
                UInt16 _value => new GraphFloatValue(Convert.ToDouble(_value)),
                Int32 _value => new GraphFloatValue(Convert.ToDouble(_value)),
                UInt32 _value => new GraphFloatValue(Convert.ToDouble(_value)),
                Int64 _value => new GraphFloatValue(Convert.ToDouble(_value)),
                UInt64 _value => new GraphFloatValue(Convert.ToDouble(_value)),

                _ => throw new NotSupportedException()
            };

        public IGraphValue CoerceInput(IGraphValue value) =>
            value switch
            {
                GraphIntValue _value => new GraphFloatValue(_value.Value),
                GraphFloatValue _value => _value,

                _ => throw new NotSupportedException($"Graph value of type {value.ValueKind} cannot be coerced into GraphFloatValue.")
            };

        public object? CoerceResult(object? value) =>
            value switch
            {
                TFloatType => Convert.ToDouble(value),
                float => value,
                double => value,
                decimal => Convert.ToDouble(value),

                SByte => Convert.ToDouble(value),
                Byte => Convert.ToDouble(value),
                Int16 => Convert.ToDouble(value),
                UInt16 => Convert.ToDouble(value),
                Int32 => Convert.ToDouble(value),
                UInt32 => Convert.ToDouble(value),
                Int64 => Convert.ToDouble(value),
                UInt64 => Convert.ToDouble(value),

                _ => throw new NotSupportedException($"Value of type '{value?.GetType().Name ?? "(null)"}' cannot be coerced into GraphFloatValue.")
            };
    }
}
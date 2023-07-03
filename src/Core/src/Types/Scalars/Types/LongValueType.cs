using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class LongValueType : IntegerValueTypeBase<Int64>
    {
        protected override Int64 MinValue => Int64.MinValue;
        protected override Int64 MaxValue => Int64.MaxValue;

        public override Int64 ToLiteral(GraphIntValue value)
            => (Int64) value.Value;

        public override GraphIntValue ToValue(Int64 value)
            => new GraphIntValue((Int32) value);
    }

    public class UnsignedLongValueType : IntegerValueTypeBase<UInt64>
    {
        protected override UInt64 MinValue => UInt64.MinValue;
        protected override UInt64 MaxValue => UInt64.MaxValue;

        public override UInt64 ToLiteral(GraphIntValue value)
            => (UInt64) value.Value;

        public override GraphIntValue ToValue(UInt64 value)
            => new GraphIntValue((Int32) value);
    }
}
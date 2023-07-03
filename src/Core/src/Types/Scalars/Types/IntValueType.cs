using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class IntValueType : IntegerValueTypeBase<Int32>
    {
        protected override Int32 MinValue => Int32.MinValue;
        protected override Int32 MaxValue => Int32.MaxValue;

        public override Int32 ToLiteral(GraphIntValue value)
            => value.Value;

        public override GraphIntValue ToValue(Int32 value)
            => new GraphIntValue(value);
    }

    public class UnsignedIntValueType : IntegerValueTypeBase<UInt32>
    {
        protected override UInt32 MinValue => UInt32.MinValue;
        protected override UInt32 MaxValue => UInt32.MaxValue;

        public override UInt32 ToLiteral(GraphIntValue value)
            => (UInt32) value.Value;

        public override GraphIntValue ToValue(UInt32 value)
            => new GraphIntValue((Int32) value);
    }
}
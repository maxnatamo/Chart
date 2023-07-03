using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class ShortValueType : IntegerValueTypeBase<Int16>
    {
        protected override Int16 MinValue => Int16.MinValue;
        protected override Int16 MaxValue => Int16.MaxValue;

        public override Int16 ToLiteral(GraphIntValue value)
            => (Int16) value.Value;

        public override GraphIntValue ToValue(Int16 value)
            => new GraphIntValue(value);
    }

    public class UnsignedShortValueType : IntegerValueTypeBase<UInt16>
    {
        protected override UInt16 MinValue => UInt16.MinValue;
        protected override UInt16 MaxValue => UInt16.MaxValue;

        public override UInt16 ToLiteral(GraphIntValue value)
            => (UInt16) value.Value;

        public override GraphIntValue ToValue(UInt16 value)
            => new GraphIntValue(value);
    }
}
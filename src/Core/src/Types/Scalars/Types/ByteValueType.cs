using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class SByteValueType : IntegerValueTypeBase<SByte>
    {
        protected override SByte MinValue => SByte.MinValue;
        protected override SByte MaxValue => SByte.MaxValue;

        public override SByte ToLiteral(GraphIntValue value)
            => (SByte) value.Value;

        public override GraphIntValue ToValue(SByte value)
            => new GraphIntValue(value);
    }

    public class ByteValueType : IntegerValueTypeBase<Byte>
    {
        protected override Byte MinValue => Byte.MinValue;
        protected override Byte MaxValue => Byte.MaxValue;

        public override Byte ToLiteral(GraphIntValue value)
            => (Byte) value.Value;

        public override GraphIntValue ToValue(Byte value)
            => new GraphIntValue(value);
    }
}
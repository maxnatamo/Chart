namespace Chart.Core
{
    public class ByteType : BaseIntegerType<sbyte>
    {
        protected override SByte MinValue => SByte.MinValue;
        protected override SByte MaxValue => SByte.MaxValue;

        public override ILeafValueResolver Resolver { get; protected set; }

        public ByteType()
            : base("Byte")
        {
            this.Description = "The Byte scalar type represents a signed 8-bit number.";

            this.Resolver = new ByteTypeResolver();
        }
    }

    public class ByteTypeResolver : BaseIntegerTypeResolver<sbyte>
    { }
}
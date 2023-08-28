namespace Chart.Core
{
    public class LongType : BaseIntegerType<long>
    {
        protected override Int64 MinValue => Int64.MinValue;
        protected override Int64 MaxValue => Int64.MaxValue;

        public override ILeafValueResolver Resolver { get; protected set; }

        public LongType()
            : base("Long")
        {
            this.Description = "The Long scalar type represents a signed 64-bit number.";

            this.Resolver = new ByteTypeResolver();
        }
    }

    public class LongTypeResolver : BaseIntegerTypeResolver<long>
    { }
}
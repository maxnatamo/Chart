namespace Chart.Core
{
    public class IntType : BaseIntegerType<int>
    {
        protected override Int32 MinValue => Int32.MinValue;
        protected override Int32 MaxValue => Int32.MaxValue;

        public override ILeafValueResolver Resolver { get; protected set; }

        public IntType()
            : base("Int")
        {
            this.Description = "The Int scalar type represents a signed 32-bit number.";

            this.Resolver = new ByteTypeResolver();
        }
    }

    public class IntTypeResolver : BaseIntegerTypeResolver<int>
    { }
}
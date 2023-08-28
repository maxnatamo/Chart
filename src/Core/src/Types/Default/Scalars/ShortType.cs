namespace Chart.Core
{
    public class ShortType : BaseIntegerType<short>
    {
        protected override Int16 MinValue => Int16.MinValue;
        protected override Int16 MaxValue => Int16.MaxValue;

        public override ILeafValueResolver Resolver { get; protected set; }

        public ShortType()
            : base("Short")
        {
            this.Description = "The Short scalar type represents a signed 16-bit number.";

            this.Resolver = new ShortTypeResolver();
        }
    }

    public class ShortTypeResolver : BaseIntegerTypeResolver<short>
    { }
}
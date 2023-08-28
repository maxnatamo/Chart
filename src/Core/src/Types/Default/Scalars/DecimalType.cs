namespace Chart.Core
{
    public class DecimalType : BaseFloatType<Decimal>
    {
        protected override Decimal MinValue => Decimal.MinValue;
        protected override Decimal MaxValue => Decimal.MaxValue;

        public override ILeafValueResolver Resolver { get; protected set; }

        public DecimalType()
            : base("Decimal")
        {
            this.Description = "The Decimal scalar type represents decimal numbers ranging from -2^96-1 to 2^96-1.";
            this.SpecifiedBy = "https://learn.microsoft.com/en-us/dotnet/api/system.decimal";

            this.Resolver = new DecimalTypeResolver();
        }
    }

    public class DecimalTypeResolver : BaseFloatTypeResolver<Decimal>
    { }
}
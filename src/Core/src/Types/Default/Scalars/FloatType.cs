namespace Chart.Core
{
    public class FloatType : BaseFloatType<Double>
    {
        protected override Double MinValue => Double.MinValue;
        protected override Double MaxValue => Double.MaxValue;

        public override ILeafValueResolver Resolver { get; protected set; }

        public FloatType()
            : base("Float")
        {
            this.Description = "The Single scalar type represents signed double-precision finite values as specified by IEEE 754.";
            this.SpecifiedBy = "https://standards.ieee.org/ieee/754/993/";

            this.Resolver = new FloatTypeResolver();
        }
    }

    public class FloatTypeResolver : BaseFloatTypeResolver<Double>
    { }
}
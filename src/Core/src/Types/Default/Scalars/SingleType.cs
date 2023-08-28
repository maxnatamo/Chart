namespace Chart.Core
{
    public class SingleType : BaseFloatType<Single>
    {
        protected override Single MinValue => Single.MinValue;
        protected override Single MaxValue => Single.MaxValue;

        public override ILeafValueResolver Resolver { get; protected set; }

        public SingleType()
            : base("Single")
        {
            this.Description = "The Single scalar type represents signed single-precision finite values as specified by IEEE 754.";
            this.SpecifiedBy = "https://standards.ieee.org/ieee/754/993/";

            this.Resolver = new SingleTypeResolver();
        }
    }

    public class SingleTypeResolver : BaseFloatTypeResolver<Single>
    { }
}
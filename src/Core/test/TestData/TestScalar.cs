using Chart.Language.SyntaxTree;

namespace Chart.Core.Tests
{
    public class TestScalar : ScalarType
    {
        public override ILeafValueResolver Resolver { get; protected set; }

        public TestScalar()
            : base(name: "TestScalar")
        {
            this.Resolver = new TestScalarResolver();
        }

        public override bool IsOfType(IGraphValue value)
            => false;

        public override bool IsOfType(object? value)
            => false;
    }

    public class TestScalarResolver : ILeafValueResolver
    {
        public object? ResolveLiteral(IGraphValue value)
            => throw new NotSupportedException();

        public IGraphValue ResolveValue(object? value)
            => throw new NotSupportedException();

        public IGraphValue CoerceInput(IGraphValue value)
            => throw new NotSupportedException();

        public object? CoerceResult(object? value)
            => throw new NotSupportedException();
    }
}
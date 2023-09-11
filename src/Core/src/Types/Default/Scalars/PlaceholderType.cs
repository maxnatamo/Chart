using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class PlaceholderType : ScalarType
    {
        public override ILeafValueResolver Resolver { get; protected set; }

        public PlaceholderType(string name)
            : base(name: name)
        {
            this.Resolver = new PlaceholderTypeResolver();
        }

        public override bool IsOfType(IGraphValue value)
            => false;

        public override bool IsOfType(object? value)
            => false;
    }

    public class PlaceholderTypeResolver : ILeafValueResolver
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
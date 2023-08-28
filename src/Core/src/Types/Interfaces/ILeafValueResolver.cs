using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public interface ILeafValueResolver
    {
        object? ResolveLiteral(IGraphValue value);

        IGraphValue ResolveValue(object? value);

        IGraphValue CoerceInput(IGraphValue value);

        object? CoerceResult(object? value);
    }
}
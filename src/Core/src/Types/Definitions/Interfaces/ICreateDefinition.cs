using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public interface ICreateDefinition<TDefinition>
        where TDefinition : IGraphNode
    {
        /// <summary>
        /// Create an instance of <typeparamref name="TDefinition" /> from the current type definition.
        /// </summary>
        TDefinition CreateSyntaxNode(IServiceProvider services);
    }
}
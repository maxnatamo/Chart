using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentTraverser
    {
        /// <summary>
        /// Descend into a GraphOperationDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphOperationDefinition definition, List<GraphNodeInfo> children)
        {
            if(definition.Variables is not null)
            {
                this.Traverse(definition.Variables, children);
            }

            if(definition.Directives is not null)
            {
                this.Traverse(definition.Directives, children);
            }

            this.Traverse(definition.Selections, children);

            return this;
        }
    }
}
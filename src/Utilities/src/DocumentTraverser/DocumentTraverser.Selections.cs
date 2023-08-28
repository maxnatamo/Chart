using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentTraverser
    {
        /// <summary>
        /// Descend into a GraphSelectionSet-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphSelectionSet definition, List<GraphNodeInfo> children)
        {
            this.TraverseChildren(nameof(definition.Selections), definition, children);

            foreach(GraphSelection selection in definition.Selections)
            {
                this.Traverse(selection, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphSelection-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphSelection definition, List<GraphNodeInfo> children)
        {
            this.TraverseChildren(nameof(definition), definition, children);

            return definition switch
            {
                GraphFieldSelection _definition => this.Traverse(_definition, children),
                GraphFragmentSelection _definition => this.Traverse(_definition, children),
                GraphInlineFragmentSelection _definition => this.Traverse(_definition, children),

                _ => throw new NotSupportedException(definition.SelectionKind.ToString())
            };
        }
    }
}
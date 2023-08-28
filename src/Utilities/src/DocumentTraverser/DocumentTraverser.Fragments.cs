using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentTraverser
    {
        /// <summary>
        /// Descend into a GraphFragmentDefinition-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphFragmentDefinition definition, List<GraphNodeInfo> children)
        {
            this.TraverseChildren(nameof(definition.Type), definition.Type, children);

            this.Traverse(definition.SelectionSet, children);

            return this;
        }

        /// <summary>
        /// Descend into a GraphFragmentSelection-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphFragmentSelection definition, List<GraphNodeInfo> children)
        {
            this.TraverseChildren(nameof(definition.Name), definition.Name, children);

            if(definition.Directives is not null)
            {
                this.Traverse(definition.Directives, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphInlineFragmentSelection-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphInlineFragmentSelection definition, List<GraphNodeInfo> children)
        {
            if(definition.TypeCondition is not null)
            {
                this.TraverseChildren(nameof(definition.TypeCondition), definition.TypeCondition, children);
            }

            if(definition.Directives is not null)
            {
                this.Traverse(definition.Directives, children);
            }

            this.Traverse(definition.SelectionSet, children);

            return this;
        }
    }
}
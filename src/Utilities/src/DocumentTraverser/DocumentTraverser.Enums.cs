using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentTraverser
    {
        /// <summary>
        /// Descend into the parent <see cref="GraphEnumDefinition" />-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphEnumDefinition node, List<GraphNodeInfo> children)
        {
            if(node.Directives is not null)
            {
                this.Traverse(node.Directives, children);
            }

            if(node.Values is not null)
            {
                this.Traverse(node.Values, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into the parent <see cref="GraphEnumDefinitionValues" />-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphEnumDefinitionValues node, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("Values", node, children);
            foreach(GraphEnumDefinitionValue definitionValue in node.Values)
            {
                this.Traverse(definitionValue, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into the parent <see cref="GraphEnumDefinitionValue" />-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphEnumDefinitionValue node, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("Value", node, children);
            this.TraverseChildren(nameof(node.Name), node.Name, children);

            if(node.Description is not null)
            {
                this.TraverseChildren(nameof(node.Description), node.Description, children);
            }

            if(node.Directives is not null)
            {
                this.Traverse(node.Directives, children);
            }

            return this;
        }
    }
}
using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentTraverser
    {
        /// <summary>
        /// Descend into a GraphInterfaceDefinition-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphInterfaceDefinition definition, List<GraphNodeInfo> children)
        {
            if(definition.Directives != null)
            {
                this.Traverse(definition.Directives, children);
            }

            if(definition.Interface != null)
            {
                this.Traverse(definition.Interface, children);
            }

            if(definition.Fields != null)
            {
                this.Traverse(definition.Fields, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphInterfaces-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphInterfaces definition, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("Interfaces", definition, children);

            this.TraverseChildren(
                nameof(definition.Implements),
                definition.Implements.Select(v => (IGraphNode) v).ToList(),
                children
            );

            return this;
        }
    }
}
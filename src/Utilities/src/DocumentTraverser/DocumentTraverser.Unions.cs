using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentTraverser
    {
        /// <summary>
        /// Descend into the parent <see cref="GraphUnionDefinition" />-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphUnionDefinition node, List<GraphNodeInfo> children)
        {
            if(node.Directives is not null)
            {
                this.Traverse(node.Directives, children);
            }

            if(node.Members is not null)
            {
                this.Traverse(node.Members, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into the parent <see cref="GraphUnionMembers" />-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphUnionMembers node, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("Members", node, children);

            this.TraverseChildren(
                "Member",
                node.Members.Select(v => (IGraphNode) v).ToList(),
                children
            );

            return this;
        }
    }
}
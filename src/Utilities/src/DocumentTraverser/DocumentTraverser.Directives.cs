using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentTraverser
    {
        /// <summary>
        /// Descend into the parent <see cref="GraphDirectiveDefinition" />-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphDirectiveDefinition node, List<GraphNodeInfo> children)
        {
            if(node.Arguments is not null)
            {
                this.Traverse(node.Arguments, children);
            }

            this.Traverse(node.Locations, children);

            return this;
        }

        /// <summary>
        /// Descend into the parent <see cref="GraphDirectiveDefinition" />-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphDirectiveLocations node, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("Locations", node, children);

            foreach(GraphDirectiveLocation location in Enum.GetValues<GraphDirectiveLocation>())
            {
                if(!node.Locations.HasFlag(location))
                {
                    continue;
                }

                this.TraverseChildren(
                    "Location",
                    new GraphName(location.ToString()),
                    children
                );
            }

            return this;
        }

        /// <summary>
        /// Descend into the parent <see cref="GraphDirectives" />-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphDirectives node, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("Directives", node, children);

            foreach(GraphDirective directive in node.Directives)
            {
                this.Traverse(directive, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into the parent <see cref="GraphDirective" />-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphDirective node, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("Directive", node, children);
            this.TraverseChildren(nameof(node.Name), node.Name, children);

            if(node.Arguments is not null)
            {
                this.Traverse(node.Arguments, children);
            }

            return this;
        }
    }
}
using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentTraverser
    {
        /// <summary>
        /// Descend into the parent <see cref="GraphArguments" />-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphArguments node, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("Arguments", node, children);

            foreach(GraphArgument argument in node.Arguments)
            {
                this.Traverse(argument, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into the parent <see cref="GraphArgument" />-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphArgument node, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("Argument", node, children);
            this.TraverseChildren(nameof(node.Name), node.Name, children);

            this.Traverse(node.Value, children);

            return this;
        }

        /// <summary>
        /// Descend into the parent <see cref="GraphArgumentsDefinition" />-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphArgumentsDefinition node, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("ArgumentsDefinition", node, children);

            foreach(GraphArgumentDefinition argument in node.Arguments)
            {
                this.Traverse(argument, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into the parent <see cref="GraphArgumentDefinition" />-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphArgumentDefinition node, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("ArgumentDefinition", node, children);
            this.TraverseChildren(nameof(node.Name), node.Name, children);

            if(node.Description is not null)
            {
                this.TraverseChildren(nameof(node.Description), node.Description, children);
            }

            if(node.Directives is not null)
            {
                this.TraverseChildren(nameof(node.Directives), node.Directives, children);
            }

            if(node.DefaultValue is not null)
            {
                this.Traverse(node.DefaultValue, children);
            }

            this.TraverseChildren(nameof(node.Type), node.Type, children);

            return this;
        }
    }
}
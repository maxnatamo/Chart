using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentTraverser
    {
        /// <summary>
        /// Descend into a GraphVariables-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphVariables definition, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("Variables", definition, children);

            foreach(GraphVariable variable in definition.Variables)
            {
                this.Traverse(variable, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphVariable-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphVariable definition, List<GraphNodeInfo> children)
        {
            this.TraverseChildren(nameof(definition.Name), definition.Name, children);
            this.TraverseChildren(nameof(definition.Type), definition.Type, children);

            if(definition.DefaultValue is not null)
            {
                this.Traverse(definition.DefaultValue, children);
            }

            if(definition.Directives is not null)
            {
                this.Traverse(definition.Directives, children);
            }

            return this;
        }
    }
}
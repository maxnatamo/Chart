using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentTraverser
    {
        /// <summary>
        /// Descend into a GraphFieldSelection-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphFieldSelection definition, List<GraphNodeInfo> children)
        {
            this.TraverseChildren(nameof(definition.Name), definition.Name, children);

            if(definition.Alias is not null)
            {
                this.TraverseChildren(nameof(definition.Alias), definition.Alias, children);
            }

            if(definition.Arguments != null)
            {
                this.Traverse(definition.Arguments, children);
            }

            if(definition.Directives != null)
            {
                this.Traverse(definition.Directives, children);
            }

            if(definition.SelectionSet != null)
            {
                this.Traverse(definition.SelectionSet, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphFields-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphFields definition, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("Fields", definition, children);
            foreach(GraphField field in definition.Fields)
            {
                this.Traverse(field, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphField-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphField definition, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("Field", definition, children);
            this.TraverseChildren(nameof(definition.Name), definition.Name, children);
            this.TraverseChildren(nameof(definition.Type), definition.Type, children);

            if(definition.Description != null)
            {
                this.TraverseChildren(nameof(definition.Description), definition.Description, children);
            }

            if(definition.Directives != null)
            {
                this.Traverse(definition.Directives, children);
            }

            if(definition.Arguments != null)
            {
                this.Traverse(definition.Arguments, children);
            }

            return this;
        }
    }
}
using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentTraverser
    {
        /// <summary>
        /// Descend into a GraphSchemaDefinition-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphSchemaDefinition definition, List<GraphNodeInfo> children)
        {
            if(definition.Directives is not null)
            {
                this.Traverse(definition.Directives, children);
            }

            this.Traverse(definition.Values, children);

            return this;
        }

        /// <summary>
        /// Descend into a GraphSchemaValues-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphSchemaValues definition, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("Values", definition, children);

            foreach(GraphSchemaValue value in definition.Values)
            {
                this.Traverse(value, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphSchemaValue-object.
        /// </summary>
        public DocumentTraverser Traverse(GraphSchemaValue definition, List<GraphNodeInfo> children)
        {
            this.TraverseChildren(nameof(definition.Operation), definition.Operation, children);
            this.TraverseChildren(nameof(definition.Type), definition.Type, children);

            return this;
        }
    }
}
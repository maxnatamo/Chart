using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentTraverser
    {
        /// <summary>
        /// Descend into a GraphTypeDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphTypeDefinition definition, List<GraphNodeInfo> children)
        {
            if(definition.Directives is not null)
            {
                this.Traverse(definition.Directives, children);
            }

            return definition switch
            {
                GraphObjectType _definition => this.Traverse(_definition, children),
                GraphScalarType _definition => this.Traverse(_definition, children),

                _ => throw new NotSupportedException(definition.TypeKind.ToString())
            };
        }

        /// <summary>
        /// Descend into a GraphInputDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphInputDefinition definition, List<GraphNodeInfo> children)
        {
            if(definition.Directives is not null)
            {
                this.Traverse(definition.Directives, children);
            }

            if(definition.Fields is not null)
            {
                this.Traverse(definition.Fields, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphObjectType-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphObjectType definition, List<GraphNodeInfo> children)
        {
            if(definition.Interface is not null)
            {
                this.Traverse(definition.Interface, children);
            }

            if(definition.Fields is not null)
            {
                this.Traverse(definition.Fields, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphScalarType-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphScalarType definition, List<GraphNodeInfo> children)
        {
            // Intentionally left empty

            return this;
        }
    }
}
using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentTraverser
    {
        /// <summary>
        /// Descend into a GraphExtensionDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphExtensionDefinition definition, List<GraphNodeInfo> children)
        {
            if(definition.Directives is not null)
            {
                this.Traverse(definition.Directives, children);
            }

            _ = definition switch
            {
                GraphScalarExtension _definition => this.Traverse(_definition, children),
                GraphObjectExtension _definition => this.Traverse(_definition, children),
                GraphInterfaceExtension _definition => this.Traverse(_definition, children),
                GraphUnionExtension _definition => this.Traverse(_definition, children),
                GraphEnumExtension _definition => this.Traverse(_definition, children),
                GraphInputExtension _definition => this.Traverse(_definition, children),
                GraphSchemaExtension _definition => this.Traverse(_definition, children),

                _ => throw new NotSupportedException(definition.ExtensionKind.ToString())
            };

            return this;
        }

        /// <summary>
        /// Descend into a GraphScalarExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphScalarExtension definition, List<GraphNodeInfo> children)
        {
            // Intentionally left empty.

            return this;
        }

        /// <summary>
        /// Descend into a GraphObjectExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphObjectExtension definition, List<GraphNodeInfo> children)
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
        /// Descend into a GraphSchemaExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphSchemaExtension definition, List<GraphNodeInfo> children)
        {
            if(definition.Values is not null)
            {
                this.Traverse(definition.Values, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphInterfaceExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphInterfaceExtension definition, List<GraphNodeInfo> children)
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
        /// Descend into a GraphUnionExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphUnionExtension definition, List<GraphNodeInfo> children)
        {
            if(definition.Members is not null)
            {
                this.Traverse(definition.Members, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphEnumExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphEnumExtension definition, List<GraphNodeInfo> children)
        {
            if(definition.Values is not null)
            {
                this.Traverse(definition.Values, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphInputExtension-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphInputExtension definition, List<GraphNodeInfo> children)
        {
            if(definition.Arguments is not null)
            {
                this.Traverse(definition.Arguments, children);
            }

            return this;
        }
    }
}
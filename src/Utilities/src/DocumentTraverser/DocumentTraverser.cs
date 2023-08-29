using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    /// <summary>
    /// Utility class for flattening a graph document into a single list
    /// of nodes.
    /// </summary>
    public partial class DocumentTraverser
    {
        /// <summary>
        /// Descend into the given definitions.
        /// </summary>
        /// <param name="definitions">The definitions to traverse.</param>
        public List<GraphNodeInfo> Traverse(IEnumerable<GraphDefinition> definitions)
        {
            List<GraphNodeInfo> children = new();

            foreach(GraphDefinition definition in definitions)
            {
                this.Traverse(definition, children);
            }

            return children;
        }

        /// <summary>
        /// Descend into a GraphDefinition-object.
        /// </summary>
        public List<GraphNodeInfo> Traverse(GraphDefinition definition, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("Definition", definition, children);
            this.TraverseChildren(nameof(definition.Name), definition.Name, children);

            if(definition.Description != null)
            {
                this.TraverseChildren(nameof(definition.Description), definition.Description, children);
            }

            _ = definition switch
            {
                GraphOperationDefinition _definition => this.Traverse(_definition, children),
                GraphTypeDefinition _definition => this.Traverse(_definition, children),
                GraphInputDefinition _definition => this.Traverse(_definition, children),
                GraphEnumDefinition _definition => this.Traverse(_definition, children),
                GraphUnionDefinition _definition => this.Traverse(_definition, children),
                GraphFragmentDefinition _definition => this.Traverse(_definition, children),
                GraphDirectiveDefinition _definition => this.Traverse(_definition, children),
                GraphExtensionDefinition _definition => this.Traverse(_definition, children),
                GraphSchemaDefinition _definition => this.Traverse(_definition, children),
                GraphInterfaceDefinition _definition => this.Traverse(_definition, children),

                _ => throw new NotSupportedException(definition.DefinitionKind.ToString())
            };

            return children;
        }

        private void TraverseChildren(
            string name,
            List<IGraphNode> nodes,
            List<GraphNodeInfo> children)
        {
            if(nodes.Count == 0)
            {
                return;
            }

            for(int i = 0; i < nodes.Count; i++)
            {
                children.Add(new GraphNodeInfo(name, nodes[i], i));
            }
        }

        private DocumentTraverser TraverseChildren(
            string name,
            IGraphNode node,
            List<GraphNodeInfo> children)
        {
            children.Add(new GraphNodeInfo(name, node));

            return this;
        }
    }
}
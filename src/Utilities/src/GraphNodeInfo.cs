using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public class GraphNodeInfo
    {
        /// <summary>
        /// Name of the field containing the node (e.g. <c>Fields</c>, <c>Arguments</c>, etc.)
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The underlying node in the document.
        /// </summary>
        public readonly IGraphNode Node;

        /// <summary>
        /// Optional index, if the node is in a list.
        /// </summary>
        public readonly int? Index = null;

        public GraphNodeInfo(string name, IGraphNode node, int? index = null)
        {
            this.Name = name;
            this.Node = node;
            this.Index = index;
        }
    }
}
using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentTraverser
    {
        /// <summary>
        /// Descend into the parent <see cref="IGraphValue" />-object.
        /// </summary>
        public DocumentTraverser Traverse(IGraphValue node, List<GraphNodeInfo> children) =>
            node switch
            {
                GraphVariableValue _value => this.TraverseChildren(nameof(_value.Value), _value, children),
                GraphIntValue _value => this.TraverseChildren(nameof(_value.Value), _value, children),
                GraphFloatValue _value => this.TraverseChildren(nameof(_value.Value), _value, children),
                GraphStringValue _value => this.TraverseChildren(nameof(_value.Value), _value, children),
                GraphBooleanValue _value => this.TraverseChildren(nameof(_value.Value), _value, children),
                GraphNullValue _value => this.TraverseChildren(nameof(_value.Value), _value, children),
                GraphEnumValue _value => this.TraverseChildren(nameof(_value.Value), _value, children),
                GraphListValue _value => this.Traverse(_value, children),
                GraphObjectValue _value => this.Traverse(_value, children),

                _ => throw new NotSupportedException(node.ValueKind.ToString())
            };

        /// <summary>
        /// Descend into a GraphEnumValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphListValue definition, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("Values", definition, children);

            foreach(IGraphValue value in definition.Value)
            {
                this.Traverse(value, children);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphObjectValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentTraverser Traverse(GraphObjectValue definition, List<GraphNodeInfo> children)
        {
            this.TraverseChildren("Values", definition, children);

            foreach(KeyValuePair<GraphName, IGraphValue> value in definition.Value)
            {
                this.TraverseChildren(nameof(value.Key), value.Key, children);
                this.Traverse(value.Value, children);
            }

            return this;
        }
    }
}
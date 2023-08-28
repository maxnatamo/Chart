namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a variable value for a variable.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Value">Original documentation</seealso>
    public interface IGraphValue : IGraphNode
    {
        /// <summary>
        /// The represented value kind.
        /// </summary>
        GraphValueKind ValueKind { get; }

        /// <summary>
        /// The underlying value of the node.
        /// </summary>
        object? Value { get; }
    }

    /// <inheritdoc />
    public interface IGraphValue<TValue> : IGraphValue
    {
        /// <inheritdoc />
        new TValue Value { get; }
    }
}
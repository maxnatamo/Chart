namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a description in the GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Name">Original documentation</seealso>
    public class GraphDescription : IGraphNode
    {
        /// <summary>
        /// The value of the name.
        /// </summary>
        public string Value { get; set; } = default!;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <summary>
        /// Create a new GraphDescription with the specified value.
        /// </summary>
        /// <param name="value">The value of the GraphDescription-node.</param>
        public GraphDescription(string value)
        {
            this.Value = value;
        }

        public static GraphDescription? From(string? value)
        {
            if(value is null)
            {
                return null;
            }

            return new GraphDescription(value);
        }

        /// <inheritdoc />
        public override string ToString() => $"[GraphDescription] {this.Value}";

        public static implicit operator string?(GraphDescription? name) => name?.Value;
    }
}
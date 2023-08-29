using System.Text.RegularExpressions;

namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a type in the GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Name">Original documentation</seealso>
    public sealed partial class GraphName : IGraphNode, ICloneable, IEquatable<GraphName>
    {
        [GeneratedRegex(@"^[a-zA-Z_]+[a-zA-Z0-9_]*$", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex GraphNameGeneratedRegex();

        /// <inheritdoc />
        public string Value { get; set; } = default!;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <summary>
        /// Create a new GraphName with the specified value.
        /// </summary>
        /// <param name="value">The value/name of the GraphName-node.</param>
        /// <exception cref="InvalidDataException">Thrown if the name is invalid.</exception>
        public GraphName(string value)
        {
            if(!GraphName.Verify(value))
            {
                throw new InvalidDataException($"Invalid GraphQL-name: {value}");
            }

            this.Value = value;
        }

        /// <summary>
        /// Verify the validity of the name.
        /// </summary>
        /// <param name="value">The name to check for.</param>
        /// <returns>True, if the value is valid. Otherwise, false.</returns>
        public static bool Verify(string value)
            => GraphNameGeneratedRegex().IsMatch(value);

        /// <inheritdoc />
        public override string ToString() => this.Value;

        /// <inheritdoc />
        public object Clone() => new GraphName(this.Value);

        /// <inheritdoc />
        public override bool Equals(object? obj) =>
            obj switch
            {
                GraphName name => this.Value == name.Value,
                string name => this.Value == name,
                null => false,
                _ => false
            };

        /// <inheritdoc />
        public bool Equals(GraphName? obj) =>
            obj switch
            {
                GraphName name => this.Value == name.Value,
                null => false
            };

        /// <summary>
        /// Calculates the hash-code of the object, for comparisons.
        /// </summary>
        /// <remarks>
        /// The hash-code is derived from the GraphName-value.
        /// </remarks>
        /// <returns>Integer-based hash-code.</returns>
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        /// <summary>
        /// Create an empty <see cref="GraphName" />-instance with no name.
        /// </summary>
        public static GraphName Empty =>
            new GraphName("_")
            {
                Value = ""
            };

        public static implicit operator string(GraphName name) => name.Value;

        public static bool operator ==(GraphName? obj1, GraphName? obj2) => obj1?.Value == obj2?.Value;
        public static bool operator !=(GraphName? obj1, GraphName? obj2) => obj1?.Value != obj2?.Value;
    }
}
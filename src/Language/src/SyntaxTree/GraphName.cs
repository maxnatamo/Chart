using System.Text.RegularExpressions;

namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a type in the GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Name">Original documentation</seealso>
    public class GraphName : IGraphNode
    {
        /// <inheritdoc />
        public string Value { get; set; } = default!;

        /// <inheritdoc />
        public GraphLocation? Location => null;

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
        {
            return Regex.IsMatch(value, @"^[a-zA-Z_]+[a-zA-Z0-9_]*$", RegexOptions.IgnoreCase);
        }

        /// <inheritdoc />
        public override string ToString() => this.Value;

        /// <summary>
        /// Compare GraphName-objects by their value.
        /// </summary>
        /// <param name="obj">The object to compare against.</param>
        /// <returns>True, if the object is a GraphName-instance and has the same value. Otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if(obj == null || obj is not GraphName)
            {
                return false;
            }
            return ((GraphName) obj).Value == this.Value;
        }

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
    }
}
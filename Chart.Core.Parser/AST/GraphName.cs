using System.Text.RegularExpressions;

namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a type in the GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Name">Original documentation</seealso>
    public class GraphName : GraphNode
    {
        /// <summary>
        /// The value of the name.
        /// </summary>
        public string Value { get; set; } = default!;

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

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return Value;
        }
    }
}
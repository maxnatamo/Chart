using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaParser
    {
        /// <summary>
        /// Parse a name on a node.
        /// </summary>
        /// <returns>The parsed GraphName-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphName ParseName()
        {
            this.Expect(TokenType.NAME);
            GraphName name = new(this.CurrentToken.Value)
            {
                Location = this.CurrentToken.Location
            };

            this.Skip();

            return name;
        }
    }
}
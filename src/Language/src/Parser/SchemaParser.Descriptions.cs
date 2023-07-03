using Chart.Language.SyntaxTree;

namespace Chart.Language.Parsers
{
    public partial class SchemaParser
    {
        /// <summary>
        /// Parse a description on a node.
        /// </summary>
        /// <returns>The parsed ParseDescription-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphDescription? ParseDescription()
        {
            if(!this.Peek(TokenType.STRING))
            {
                return null;
            }

            this.Expect(TokenType.STRING);
            GraphDescription def = new GraphDescription(this.CurrentToken.Value);
            this.Skip();

            if(this.Options.Ignore.HasFlag(SchemaParserIgnore.Descriptions))
            {
                return null;
            }

            return def;
        }
    }
}
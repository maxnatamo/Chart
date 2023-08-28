using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaParser
    {
        /// <summary>
        /// Parse the arguments defined in a selection.
        /// </summary>
        /// <returns>The parsed GraphArguments-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphArguments ParseArguments()
        {
            GraphArguments def = new GraphArguments();
            def.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.PARENTHESIS_LEFT);
            this.Skip();

            def.Arguments = this.IterateParse<GraphArgument>(this.ParseArgument);

            this.Expect(TokenType.PARENTHESIS_RIGHT);
            this.Skip();

            return def;
        }

        /// <summary>
        /// Parse an argument inside of a selection argument list.
        /// </summary>
        /// <returns>A non-null GraphArgument, if a selection was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphArgument? ParseArgument()
        {
            if(this.Peek(TokenType.PARENTHESIS_RIGHT))
            {
                return null;
            }

            GraphArgument def = new GraphArgument();
            def.Location = this.CurrentToken.Location.Clone();
            def.Name = this.ParseName();

            this.Expect(TokenType.COLON);
            this.Skip();

            def.Value = this.ParseValue();

            if(this.Peek(TokenType.EQUAL))
            {
                throw new DefaultValuesNotAllowedException(this.CurrentToken.Start);
            }

            return def;
        }
    }
}
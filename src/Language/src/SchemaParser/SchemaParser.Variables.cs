using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaParser
    {
        /// <summary>
        /// Parse the variables defined in a GraphQL-operation.
        /// </summary>
        /// <returns>The parsed GraphVariables-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphVariables ParseVariables()
        {
            GraphVariables def = new GraphVariables();
            def.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.PARENTHESIS_LEFT);
            this.Skip();

            def.Variables = this.IterateParse<GraphVariable>(this.ParseVariable);

            this.Expect(TokenType.PARENTHESIS_RIGHT);
            this.Skip();

            return def;
        }

        /// <summary>
        /// Parse a variable inside of a GraphQL-operation variable list.
        /// </summary>
        /// <returns>A non-null GraphVariable, if a selection was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphVariable? ParseVariable()
        {
            if(this.Peek(TokenType.PARENTHESIS_RIGHT))
            {
                return null;
            }

            GraphVariable def = new GraphVariable();
            def.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.DOLLAR_SIGN);
            this.Skip();

            def.Name = this.ParseName();

            this.Expect(TokenType.COLON);
            this.Skip();

            def.Type = this.ParseType();

            if(this.Peek(TokenType.EQUAL))
            {
                this.Skip();
                def.DefaultValue = this.ParseValue(true);
            }

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            return def;
        }
    }
}
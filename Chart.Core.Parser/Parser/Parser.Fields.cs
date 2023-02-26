namespace Chart.Core.Parser
{
    public partial class Parser
    {
        /// <summary>
        /// Parse the fields inside a type-definition in the GraphQL-document.
        /// </summary>
        /// <returns>The parsed GraphFieldsDefinition-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphFields ParseFieldsDefinition()
        {
            GraphFields def = new GraphFields();

            this.Expect(TokenType.BRACE_LEFT);
            this.Skip();

            def.Fields = this.IterateParse<GraphField>(this.ParseFieldDefinition);

            this.Expect(TokenType.BRACE_RIGHT);
            this.Skip();

            return def;
        }

        /// <summary>
        /// Parse a field inside a type-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphField, if a definition was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphField? ParseFieldDefinition()
        {
            if(this.Peek(TokenType.BRACE_RIGHT))
            {
                return null;
            }

            GraphField def = new GraphField();

            if(this.Peek(TokenType.STRING))
            {
                def.Description = new GraphDescription(this.CurrentToken.Value);
                this.Skip();
            }

            this.Expect(TokenType.NAME);
            def.Name = new GraphName(this.CurrentToken.Value);

            // Skip name of type
            this.Skip();

            if(this.Peek(TokenType.PARENTHESIS_LEFT))
            {
                def.Arguments = this.ParseArgumentsDefinition();
            }

            this.Expect(TokenType.COLON);
            this.Skip();

            def.Type = this.ParseType();

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            return def;
        }
    }
}
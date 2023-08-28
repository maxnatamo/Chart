using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaParser
    {
        /// <summary>
        /// Parse the fields inside a type-definition in the GraphQL-document.
        /// </summary>
        /// <returns>The parsed GraphFieldsDefinition-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphFields ParseFieldsDefinition()
        {
            GraphFields def = new GraphFields();
            def.Location = this.CurrentToken.Location.Clone();

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
            def.Description = this.ParseDescription();
            def.Location = this.CurrentToken.Location.Clone();
            def.Name = this.ParseName();

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
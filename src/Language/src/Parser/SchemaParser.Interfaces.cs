using Chart.Language.SyntaxTree;

namespace Chart.Language.Parsers
{
    public partial class SchemaParser
    {
        /// <summary>
        /// Parse a top-level interface-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphInterfaceDefinition, if an interface was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphInterfaceDefinition? ParseInterfaceDefinition()
        {
            GraphInterfaceDefinition def = new GraphInterfaceDefinition();

            this.Expect(TokenType.NAME);
            this.Expect("interface");

            // Skip 'interface'-keyword
            this.Skip();

            this.Expect(TokenType.NAME);
            def.Name = new GraphName(this.CurrentToken.Value);
            this.Skip();

            if(this.Peek(TokenType.NAME) && this.Peek("implements"))
            {
                def.Interface = this.ParseInterfaces();
            }

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            if(this.Peek(TokenType.BRACE_LEFT))
            {
                def.Fields = this.ParseFieldsDefinition();
            }

            return def;
        }

        /// <summary>
        /// Parse an interface implementation in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphInterfaces.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphInterfaces ParseInterfaces()
        {
            GraphInterfaces def = new GraphInterfaces();

            this.Expect(TokenType.NAME);
            this.Expect("implements");

            // Skip 'implements'-keyword
            this.Skip();

            while(true)
            {
                if(this.Peek(TokenType.EOF))
                {
                    throw new UnexpectedTokenException(this.CurrentToken.Start);
                }

                this.Expect(TokenType.NAME);
                GraphNamedType type = new GraphNamedType(this.CurrentToken.Value);
                this.Skip();

                def.Implements.Add(type);

                if(!this.Peek(TokenType.AMPERSAND))
                {
                    break;
                }

                this.Expect(TokenType.AMPERSAND);
                this.Skip();
            }

            return def;
        }
    }
}
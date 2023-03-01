using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class SchemaParser
    {
        /// <summary>
        /// Parse a top-level enum-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphEnumDefinition, if a type was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphEnumDefinition? ParseEnumDefinition()
        {
            GraphEnumDefinition def = new GraphEnumDefinition();

            this.Expect(TokenType.NAME);
            this.Expect("enum");

            // Skip 'type'-keyword
            this.Skip();

            // Read name
            this.Expect(TokenType.NAME);
            def.Name = new GraphName(this.CurrentToken.Value);

            this.Skip();

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            // Read fields
            def.Values = this.ParseEnumValuesDefinition();

            return def;
        }

        /// <summary>
        /// Parse the nodes inside an enum-definition in the GraphQL-document.
        /// </summary>
        /// <returns>The parsed GraphEnumDefinitionValues-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphEnumDefinitionValues ParseEnumValuesDefinition()
        {
            this.Expect(TokenType.BRACE_LEFT);
            this.Skip();

            GraphEnumDefinitionValues def = new GraphEnumDefinitionValues
            {
                Values = this.IterateParse<GraphEnumDefinitionValue>(this.ParseEnumValueDefinition)
            };

            this.Expect(TokenType.BRACE_RIGHT);
            this.Skip();

            return def;
        }

        /// <summary>
        /// Parse a field inside an enum-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphEnumDefinitionValue, if a definition was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphEnumDefinitionValue? ParseEnumValueDefinition()
        {
            if(this.CurrentToken.Type == TokenType.BRACE_RIGHT)
            {
                return null;
            }

            GraphEnumDefinitionValue def = new GraphEnumDefinitionValue();

            if(this.Peek(TokenType.STRING))
            {
                def.Description = new GraphDescription(this.CurrentToken.Value);
                this.Skip();
            }

            this.Expect(TokenType.NAME);
            def.Name = new GraphName(this.CurrentToken.Value);

            this.Skip();

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            return def;
        }
    }
}
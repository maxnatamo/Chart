using Chart.Language.SyntaxTree;

namespace Chart.Language
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
            def.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.NAME);
            this.Expect("enum");

            // Skip 'type'-keyword
            this.Skip();

            // Read name
            def.Name = this.ParseName();

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
                Location = this.CurrentToken.Location.Clone(),
                Values = this.IterateParse<GraphEnumDefinitionValue>(this.ParseEnumValueDefinition),
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
            def.Description = this.ParseDescription();
            def.Location = this.CurrentToken.Location.Clone();
            def.Name = this.ParseName();

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            return def;
        }
    }
}
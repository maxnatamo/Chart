using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaParser
    {
        /// <summary>
        /// Parse a top-level object-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphObjectType, if a type was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphSchemaDefinition? ParseSchemaDefinition()
        {
            GraphSchemaDefinition def = new GraphSchemaDefinition();
            def.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.NAME);
            this.Expect("schema");

            // Skip 'schema'-keyword
            this.Skip();

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            // Read fields
            def.Values = this.ParseSchemaValues();

            return def;
        }

        /// <summary>
        /// Parse the values inside a schema-definition in the GraphQL-document.
        /// </summary>
        /// <returns>The parsed GraphSchemaValues-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphSchemaValues ParseSchemaValues()
        {
            GraphSchemaValues def = new GraphSchemaValues();
            def.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.BRACE_LEFT);
            this.Skip();

            def.Values = this.IterateParse<GraphSchemaValue>(this.ParseSchemaValue);

            this.Expect(TokenType.BRACE_RIGHT);
            this.Skip();

            return def;
        }

        /// <summary>
        /// Parse a value inside a schema-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphSchemaValue, if a value was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphSchemaValue? ParseSchemaValue()
        {
            if(this.Peek(TokenType.BRACE_RIGHT))
            {
                return null;
            }

            GraphSchemaValue def = new GraphSchemaValue();
            def.Location = this.CurrentToken.Location.Clone();
            def.Operation = this.ParseName();

            this.Expect(TokenType.COLON);
            this.Skip();

            this.Expect(TokenType.NAME);
            def.Type = new GraphNamedType(this.CurrentToken.Value);
            this.Skip();

            return def;
        }
    }
}
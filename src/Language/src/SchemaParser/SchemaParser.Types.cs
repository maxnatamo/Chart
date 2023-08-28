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
        public GraphObjectType? ParseObjectTypeDefinition()
        {
            GraphObjectType def = new()
            {
                Location = this.CurrentToken.Location.Clone()
            };

            this.Expect(TokenType.NAME);
            this.Expect("type");

            // Skip 'type'-keyword
            this.Skip();

            // Read name
            def.Name = this.ParseName();

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
        /// Parse a top-level input-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphInputDefinition, if a type was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphInputDefinition? ParseInputTypeDefinition()
        {
            GraphInputDefinition def = new()
            {
                Location = this.CurrentToken.Location.Clone()
            };

            this.Expect(TokenType.NAME);
            this.Expect("input");

            // Skip 'input'-keyword
            this.Skip();

            // Read name
            def.Name = this.ParseName();

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            if(this.Peek(TokenType.BRACE_LEFT))
            {
                def.Fields = (GraphInputFieldsDefinition) this.ParseArgumentsDefinition(
                    TokenType.BRACE_LEFT,
                    TokenType.BRACE_RIGHT
                );
            }

            return def;
        }

        /// <summary>
        /// Parse a top-level scalar-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphScalarType, if a scalar was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphScalarType? ParseScalarTypeDefinition()
        {
            GraphScalarType def = new()
            {
                Location = this.CurrentToken.Location.Clone()
            };

            this.Expect(TokenType.NAME);
            this.Expect("scalar");

            // Skip 'type'-keyword
            this.Skip();

            // Read name
            def.Name = new GraphName(this.CurrentToken.Value);

            this.Skip();

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            return def;
        }

        /// <summary>
        /// Parse a type inside a type-definition in the GraphQL-document.
        /// </summary>
        /// <returns>The parsed GraphType-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphType ParseType()
        {
            return this.CurrentToken.Type switch
            {
                TokenType.NAME => this.ParseNamedType(),
                TokenType.BRACKET_LEFT => this.ParseListType(),

                _ => throw this.UnexpectedToken()
            };
        }

        /// <summary>
        /// Parse a named type inside a type-definition in the GraphQL-document.
        /// </summary>
        /// <returns>The parsed GraphNamedType-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphNamedType ParseNamedType()
        {
            this.Expect(TokenType.NAME);

            GraphNamedType def = new()
            {
                Name = new GraphName(this.CurrentToken.Value),
                Location = this.CurrentToken.Location.Clone()
            };

            // Skip name
            this.Skip();

            if(this.Peek(TokenType.EXCLAMATION_POINT))
            {
                def.NonNullable = true;
                this.Skip();
            }

            return def;
        }

        /// <summary>
        /// Parse a list type inside a type-definition in the GraphQL-document.
        /// </summary>
        /// <returns>The parsed GraphListType-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphListType ParseListType()
        {
            GraphListType def = new()
            {
                Location = this.CurrentToken.Location.Clone()
            };

            this.Expect(TokenType.BRACKET_LEFT);
            this.Skip();

            def.UnderlyingType = this.CurrentToken.Type switch
            {
                TokenType.NAME => this.ParseNamedType(),
                TokenType.BRACKET_LEFT => this.ParseListType(),

                _ => throw this.UnexpectedToken()
            };

            this.Expect(TokenType.BRACKET_RIGHT);
            this.Skip();

            if(this.Peek(TokenType.EXCLAMATION_POINT))
            {
                def.NonNullable = true;
                this.Skip();
            }

            return def;
        }
    }
}
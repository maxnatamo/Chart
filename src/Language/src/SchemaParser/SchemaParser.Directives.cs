using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaParser
    {
        /// <summary>
        /// List of all valid directive locations, as per the spec.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#DirectiveLocation">Original documentation</seealso>
        private static readonly string[] VALID_DIRECTIVE_LOCATIONS =
        {
            // ExecutableDirectiveLocation
            "QUERY",
            "MUTATION",
            "SUBSCRIPTION",
            "FIELD",
            "FRAGMENT_DEFINITION",
            "FRAGMENT_SPREAD",
            "INLINE_FRAGMENT",
            "VARIABLE_DEFINITION",

            // TypeSystemDirectiveLocation
            "SCHEMA",
            "SCALAR",
            "OBJECT",
            "FIELD_DEFINITION",
            "ARGUMENT_DEFINITION",
            "INTERFACE",
            "UNION",
            "ENUM",
            "ENUM_VALUE",
            "INPUT_OBJECT",
            "INPUT_FIELD_DEFINITION",
        };

        /// <summary>
        /// Parse a top-level enum-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphEnumDefinition, if a type was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphDirectiveDefinition? ParseDirectiveDefinition()
        {
            GraphDirectiveDefinition def = new GraphDirectiveDefinition();
            def.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.NAME);
            this.Expect("directive");
            this.Skip();

            this.Expect(TokenType.AT);
            this.Skip();

            // Read name
            def.Name = this.ParseName();

            if(this.Peek(TokenType.PARENTHESIS_LEFT))
            {
                def.Arguments = this.ParseArgumentsDefinition();
            }

            if(this.Peek(TokenType.NAME) && this.Peek("repeatable"))
            {
                def.Repeatable = true;
                this.Skip();
            }

            this.Expect(TokenType.NAME);
            this.Expect("on");
            this.Skip();

            def.Locations = this.ParseDirectiveLocations();
            if(def.Locations.Locations == 0)
            {
                throw new EmptyDirectiveException(this.CurrentToken.Start);
            }

            return def;
        }

        /// <summary>
        /// Parse the locations defined in a directive.
        /// </summary>
        /// <returns>The parsed GraphDirectiveLocations-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphDirectiveLocations ParseDirectiveLocations()
        {
            GraphDirectiveLocations locations = new GraphDirectiveLocations();
            locations.Location = this.CurrentToken.Location.Clone();

            if(!this.Peek(TokenType.NAME))
            {
                return locations;
            }

            while(true)
            {
                if(this.Peek(TokenType.EOF))
                {
                    throw new UnexpectedTokenException(this.CurrentToken.Start);
                }

                this.Expect(TokenType.NAME);

                if(!Enum.TryParse<GraphDirectiveLocation>(this.CurrentToken.Value, out GraphDirectiveLocation location))
                {
                    throw new InvalidDirectiveLocationException(this.CurrentToken.Value);
                }

                locations.Locations |= location;

                this.Skip();

                if(!this.Peek(TokenType.PIPE))
                {
                    break;
                }

                this.Expect(TokenType.PIPE);
                this.Skip();
            }

            return locations;
        }

        /// <summary>
        /// Parse directives from a definition.
        /// </summary>
        /// <returns>The parsed GraphDirectives-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphDirectives ParseDirectives()
        {
            GraphDirectives def = new GraphDirectives();
            def.Location = this.CurrentToken.Location.Clone();

            def.Directives = this.IterateParse<GraphDirective>(this.ParseDirective);

            return def;
        }

        /// <summary>
        /// Parse directives from a definition.
        /// </summary>
        /// <returns>A non-null GraphDirective, if a directive was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphDirective? ParseDirective()
        {
            if(!this.Peek(TokenType.AT))
            {
                return null;
            }

            GraphDirective def = new GraphDirective();
            def.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.AT);
            this.Skip();

            def.Name = this.ParseName();

            if(this.Peek(TokenType.PARENTHESIS_LEFT))
            {
                def.Arguments = this.ParseArguments();
            }

            return def;
        }
    }
}
using Chart.Models.AST;
using Chart.Shared.Exceptions;

namespace Chart.Core.Parsers
{
    public partial class SchemaParser
    {
        /// <summary>
        /// List of all valid directive locations, as per the spec.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#DirectiveLocation">Original documentation<seealso>
        private static string[] VALID_DIRECTIVE_LOCATIONS =
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

            this.Expect(TokenType.NAME);
            this.Expect("directive");
            this.Skip();

            this.Expect(TokenType.AT);
            this.Skip();

            // Read name
            this.Expect(TokenType.NAME);
            def.Name = new GraphName(this.CurrentToken.Value);

            this.Skip();

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
            if(def.Locations.Locations.Count == 0)
            {
                throw new EmptyDirectiveException(this.CurrentToken.Start);
            }

            // Check for valid locations
            foreach(var loc in def.Locations.Locations)
            {
                if(!VALID_DIRECTIVE_LOCATIONS.Contains(loc.Value))
                {
                    throw new InvalidDirectiveLocationException(loc.Value);
                }
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
                GraphName loc = new GraphName(this.CurrentToken.Value);
                this.Skip();

                locations.Locations.Add(loc);

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

            this.Expect(TokenType.AT);
            this.Skip();

            this.Expect(TokenType.NAME);
            def.Name = new GraphName(this.CurrentToken.Value);
            this.Skip();

            if(this.Peek(TokenType.PARENTHESIS_LEFT))
            {
                def.Arguments = this.ParseArguments();
            }

            return def;
        }
    }
}
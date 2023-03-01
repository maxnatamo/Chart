using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class SchemaParser
    {
        /// <summary>
        /// Parse the arguments defined in a selection.
        /// <para>
        /// Some situations calls for different opening- and closing-tokens.
        /// For example, input-types use braces and fields use parenthesis.
        /// This method allows for those situations, but defaults to parenthesis.
        /// </para>
        /// </summary>
        /// <param name="start">The opening token for the arguments.</param>
        /// <param name="end">The closing token for the arguments.</param>
        /// <returns>The parsed GraphArguments-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphArgumentsDefinition ParseArgumentsDefinition(
            TokenType start = TokenType.PARENTHESIS_LEFT,
            TokenType end = TokenType.PARENTHESIS_RIGHT)
        {
            GraphArgumentsDefinition def = new GraphArgumentsDefinition();

            this.Expect(start);
            this.Skip();

            def.Arguments = this.IterateParse<GraphArgumentDefinition>(() =>
            {
                return this.ParseArgumentDefinition(end);
            });

            this.Expect(end);
            this.Skip();

            return def;
        }

        /// <summary>
        /// Parse an argument inside of a selection argument list.
        /// </summary>
        /// <returns>A non-null GraphArgument, if a selection was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphArgumentDefinition? ParseArgumentDefinition(TokenType end = TokenType.PARENTHESIS_RIGHT)
        {
            if(this.Peek(end))
            {
                return null;
            }

            GraphArgumentDefinition def = new GraphArgumentDefinition();

            if(this.Peek(TokenType.STRING))
            {
                def.Description = new GraphDescription(this.CurrentToken.Value);
                this.Skip();
            }

            this.Expect(TokenType.NAME);
            def.Name = new GraphName(this.CurrentToken.Value);
            this.Skip();

            this.Expect(TokenType.COLON);
            this.Skip();

            def.Type = this.ParseType();

            if(this.Peek(TokenType.EQUAL))
            {
                this.Skip();

                def.DefaultValue = this.ParseValue();
            }

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            return def;
        }
    }
}
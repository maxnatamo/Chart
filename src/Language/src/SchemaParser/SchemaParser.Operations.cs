using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaParser
    {
        /// <summary>
        /// Parse a top-level operation definition in the GraphQL-document, such as queries, mutations and subscriptions.
        /// </summary>
        /// <returns>A non-null GraphOperationDefinition, if a definition was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphOperationDefinition? ParseOperationDefinition()
        {
            GraphOperationDefinition def = this.CurrentToken.Value switch
            {
                "{" => new GraphQueryOperation(),
                "query" => new GraphQueryOperation(),
                "mutation" => new GraphMutationOperation(),
                "subscription" => new GraphSubscriptionOperation(),

                // If nothing is specified, default to query-operation.
                _ => new GraphQueryOperation()
            };

            def.Location = this.CurrentToken.Location.Clone();

            // Skip operation type
            if(this.Peek(TokenType.NAME))
            {
                this.Skip();
            }

            if(this.Peek(TokenType.NAME))
            {
                def.Name = this.ParseName();
            }

            if(this.Peek(TokenType.PARENTHESIS_LEFT))
            {
                def.Variables = this.ParseVariables();
            }

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            this.Expect(TokenType.BRACE_LEFT);

            def.Selections = this.ParseSelectionSet();

            return def;
        }
    }
}
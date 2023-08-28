using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaParser
    {
        /// <summary>
        /// Parse a selection-set from a GraphQL-operation
        /// </summary>
        /// <returns>The parsed GraphSelectionSet-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphSelectionSet ParseSelectionSet()
        {
            GraphSelectionSet def = new()
            {
                Location = this.CurrentToken.Location.Clone()
            };

            this.Expect(TokenType.BRACE_LEFT);
            this.Skip();

            def.Selections = this.IterateParse<GraphSelection>(this.ParseSelection);

            // Empty selections are not allowed in GraphQL, per the spec.
            // Source: https://github.com/graphql/graphql-spec/blame/October2021/spec/Section%203%20--%20Type%20System.md#L663-L667
            if(def.Selections.Count == 0)
            {
                throw new EmptySelectionException(this.CurrentToken);
            }

            this.Expect(TokenType.BRACE_RIGHT);

            // Skip brace
            this.Skip();

            return def;
        }

        /// <summary>
        /// Parse a single selection inside of a selection-set.
        /// </summary>
        /// <returns>A non-null GraphSelection, if a selection was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphSelection? ParseSelection()
        {
            if(this.Peek(TokenType.NAME))
            {
                return this.ParseFieldSelection();
            }

            if(this.Peek(TokenType.SPREAD))
            {
                this.Skip();

                // Fragment spreads require a name that isn't "on",
                // so we can differentiate between spreads and inlines with the next token.
                return (this.Peek(TokenType.NAME) && !this.Peek("on"))
                    ? this.ParseFragmentSpreadSelection()
                    : this.ParseInlineFragmentSelection();
            }

            return null;
        }

        /// <summary>
        /// Parse a field-selection from the current selection.
        /// </summary>
        /// <returns>The parsed GraphFieldSelection-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphFieldSelection ParseFieldSelection()
        {
            GraphFieldSelection def = new()
            {
                Location = this.CurrentToken.Location.Clone()
            };

            string firstName = this.CurrentToken.Value;
            this.Skip();

            // Handle aliases
            if(this.Peek(TokenType.COLON))
            {
                this.Skip();

                def.Alias = new GraphName(firstName);
                def.Name = this.ParseName();
            }
            else
            {
                def.Name = new GraphName(firstName);
            }

            if(this.Peek(TokenType.PARENTHESIS_LEFT))
            {
                def.Arguments = this.ParseArguments();
            }

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            if(this.Peek(TokenType.BRACE_LEFT))
            {
                def.SelectionSet = this.ParseSelectionSet();
            }

            return def;
        }
    }
}
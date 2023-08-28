using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaParser
    {
        /// <summary>
        /// Parse a top-level fragment-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphFragmentDefinition, if a type was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphFragmentDefinition? ParseFragmentDefinition()
        {
            GraphFragmentDefinition def = new GraphFragmentDefinition();
            def.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.NAME);
            this.Expect("fragment");

            // Skip 'type'-keyword
            this.Skip();

            // Read name
            def.Name = this.ParseName();

            // Read condition
            this.Expect(TokenType.NAME);
            this.Expect("on");
            this.Skip();

            // Read fragment type
            this.Expect(TokenType.NAME);
            def.Type = new GraphNamedType(this.CurrentToken.Value);
            def.Type.Location = this.CurrentToken.Location.Clone();
            this.Skip();

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            // Read selection
            def.SelectionSet = this.ParseSelectionSet();

            return def;
        }

        /// <summary>
        /// Parse a fragment-selection from the current selection.
        /// </summary>
        /// <returns>The parsed GraphFragmentSelection-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphFragmentSelection ParseFragmentSpreadSelection()
        {
            GraphFragmentSelection def = new GraphFragmentSelection();
            def.Location = this.CurrentToken.Location.Clone();
            def.Name = this.ParseName();

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            return def;
        }

        /// <summary>
        /// Parse an inline-fragment from the current selection.
        /// </summary>
        /// <returns>The parsed GraphInlineFragmentSelection-object.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphInlineFragmentSelection ParseInlineFragmentSelection()
        {
            GraphInlineFragmentSelection def = new GraphInlineFragmentSelection();
            def.Location = this.CurrentToken.Location.Clone();

            if(this.Peek(TokenType.NAME) && this.Peek("on"))
            {
                this.Skip();

                def.TypeCondition = new GraphNamedType(this.CurrentToken.Value);
                this.Skip();
            }

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            def.SelectionSet = this.ParseSelectionSet();

            return def;
        }
    }
}
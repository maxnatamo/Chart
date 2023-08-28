using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaParser
    {
        /// <summary>
        /// Parse a top-level union-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphUnionDefinition, if a union was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphUnionDefinition? ParseUnionDefinition()
        {
            GraphUnionDefinition def = new GraphUnionDefinition();
            def.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.NAME);
            this.Expect("union");

            // Skip 'union'-keyword
            this.Skip();

            // Read name
            def.Name = new GraphName(this.CurrentToken.Value);

            this.Skip();

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            if(!this.Peek(TokenType.EQUAL))
            {
                return def;
            }

            def.Members = new GraphUnionMembers();
            def.Members.Location = this.CurrentToken.Location.Clone();

            this.Expect(TokenType.EQUAL);
            this.Skip();

            while(true)
            {
                if(this.Peek(TokenType.EOF))
                {
                    throw new UnexpectedTokenException(this.CurrentToken.Start);
                }

                GraphName member = this.ParseName();

                def.Members.Members.Add(member);

                if(!this.Peek(TokenType.PIPE))
                {
                    break;
                }

                this.Expect(TokenType.PIPE);
                this.Skip();
            }

            return def;
        }
    }
}
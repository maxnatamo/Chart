using Chart.Models.AST;
using Chart.Shared.Exceptions;

namespace Chart.Core.Parsers
{
    public partial class SchemaParser
    {
        /// <summary>
        /// Parse a top-level extension-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphExtensionDefinition.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphExtensionDefinition ParseExtensionDefinition()
        {
            this.Expect(TokenType.NAME);
            this.Expect("extend");

            // Skip 'extend'-keyword
            this.Skip();

            return this.CurrentToken.Value switch
            {
                "scalar"    => this.ParseScalarExtension(),
                "type"      => this.ParseObjectExtension(),
                "schema"    => this.ParseSchemaExtension(),
                "interface" => this.ParseInterfaceExtension(),
                "union"     => this.ParseUnionExtension(),
                "enum"      => this.ParseEnumExtension(),
                "input"     => this.ParseInputExtension(),

                _           => throw UnexpectedToken()
            };
        }

        /// <summary>
        /// Parse a top-level extension-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphExtensionDefinition.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphScalarExtension ParseScalarExtension()
        {
            this.Expect(TokenType.NAME);
            this.Expect("scalar");

            // Skip 'scalar'-keyword
            this.Skip();

            GraphScalarExtension def = new GraphScalarExtension();
            def.Name = new GraphName(this.CurrentToken.Value);
            this.Skip();

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            return def;
        }

        /// <summary>
        /// Parse a top-level extension-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphObjectExtension.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphObjectExtension ParseObjectExtension()
        {
            this.Expect(TokenType.NAME);
            this.Expect("type");

            // Skip 'type'-keyword
            this.Skip();

            GraphObjectExtension def = new GraphObjectExtension();
            def.Name = new GraphName(this.CurrentToken.Value);
            this.Skip();

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
        /// Parse a top-level extension-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphSchemaExtension.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphSchemaExtension ParseSchemaExtension()
        {
            this.Expect(TokenType.NAME);
            this.Expect("schema");

            // Skip 'schema'-keyword
            this.Skip();

            GraphSchemaExtension def = new GraphSchemaExtension();

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }

            if(this.Peek(TokenType.BRACE_LEFT))
            {
                def.Values = this.ParseSchemaValues();
            }

            return def;
        }

        /// <summary>
        /// Parse a top-level extension-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphInterfaceExtension.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphInterfaceExtension ParseInterfaceExtension()
        {
            this.Expect(TokenType.NAME);
            this.Expect("interface");

            // Skip 'interface'-keyword
            this.Skip();

            GraphInterfaceExtension def = new GraphInterfaceExtension();
            def.Name = new GraphName(this.CurrentToken.Value);
            this.Skip();

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
        /// Parse a top-level extension-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphUnionExtension.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphUnionExtension ParseUnionExtension()
        {
            this.Expect(TokenType.NAME);
            this.Expect("union");

            // Skip 'union'-keyword
            this.Skip();

            GraphUnionExtension def = new GraphUnionExtension();
            def.Name = new GraphName(this.CurrentToken.Value);
            this.Skip();

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }
            
            if(this.Peek(TokenType.EQUAL))
            {
                def.Members = new GraphUnionMembers();
                
                this.Skip();

                while(true)
                {
                    if(this.Peek(TokenType.EOF))
                    {
                        throw new UnexpectedTokenException(this.CurrentToken.Start);
                    }

                    this.Expect(TokenType.NAME);
                    GraphName member = new GraphName(this.CurrentToken.Value);
                    this.Skip();

                    def.Members.Members.Add(member);

                    if(!this.Peek(TokenType.PIPE))
                    {
                        break;
                    }

                    this.Expect(TokenType.PIPE);
                    this.Skip();
                }
            }

            return def;
        }

        /// <summary>
        /// Parse a top-level extension-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphEnumExtension.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphEnumExtension ParseEnumExtension()
        {
            this.Expect(TokenType.NAME);
            this.Expect("enum");

            // Skip 'enum'-keyword
            this.Skip();

            GraphEnumExtension def = new GraphEnumExtension();
            def.Name = new GraphName(this.CurrentToken.Value);
            this.Skip();

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }
            
            if(this.Peek(TokenType.BRACE_LEFT))
            {
                def.Values = this.ParseEnumValuesDefinition();
            }

            return def;
        }

        /// <summary>
        /// Parse a top-level extension-definition in the GraphQL-document.
        /// </summary>
        /// <returns>A non-null GraphInputExtension.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        private GraphInputExtension ParseInputExtension()
        {
            this.Expect(TokenType.NAME);
            this.Expect("input");

            // Skip 'input'-keyword
            this.Skip();

            GraphInputExtension def = new GraphInputExtension();
            def.Name = new GraphName(this.CurrentToken.Value);
            this.Skip();

            if(this.Peek(TokenType.AT))
            {
                def.Directives = this.ParseDirectives();
            }
            
            if(this.Peek(TokenType.BRACE_LEFT))
            {
                def.Arguments = ParseArgumentsDefinition(
                    TokenType.BRACE_LEFT,
                    TokenType.BRACE_RIGHT
                );
            }

            return def;
        }
    }
}
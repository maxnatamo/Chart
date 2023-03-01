using Chart.Models.AST;
using Chart.Shared.Exceptions;

namespace Chart.Core.Parsers
{
    /// <summary>
    /// The parser is split up into separate files, for better oversight.
    /// 
    /// The parsing methods are made to be iterative, meaning they parse a set of
    /// tokens, continue to the next tokens and repeat.
    /// 
    /// Parsing methods that are expected to return multiple definitions return a nullable
    /// version of the definition-type, and returns null when there's no definitions left in the
    /// current context.
    /// 
    /// Instead of indexing characters in the source, the parser indexes the tokens.
    /// </summary>
    public partial class SchemaParser
    {
        /// <summary>
        /// The options used for parsing.
        /// </summary>
        private SchemaParserOptions Options = new SchemaParserOptions();

        /// <summary>
        /// The source document.
        /// </summary>
        public Tokenizer Tokenizer = new Tokenizer();

        /// <summary>
        /// The current depth of the document.
        /// </summary>
        private int CurrentDepth { get; set; } = 0;

        /// <summary>
        /// The parent document for the parsing.
        /// </summary>
        public GraphDocument Document { get; protected set; } = default!;

        /// <summary>
        /// The current position into the token-list
        /// </summary>
        private int CurrentTokenIndex { get; set; } = 0;

        /// <summary>
        /// The currently-processing token
        /// </summary>
        private Token CurrentToken = new Token();

        /// <summary>
        /// Increase the current depth of the document.
        /// </summary>
        /// <exception cref="MaxDepthReachedException">Thrown when the maximum depth of querying has been reached.</exception>
        private void IncreaseDepth()
        {
            this.CurrentDepth++;

            if(this.CurrentDepth > this.Options.MaxDepth)
            {
                throw new MaxDepthReachedException(this.CurrentDepth);
            }
        }

        /// <summary>
        /// Decrease the current depth of the document.
        /// </summary>
        /// <exception cref="UnmatchedBraceException">Thrown when the depth of the document becomes negative.</exception>
        private void DecreaseDepth()
        {
            this.CurrentDepth--;

            if(this.CurrentDepth < 0)
            {
                throw new UnmatchedBraceException(this.CurrentToken.Start);
            }
        }

        /// <summary>
        /// Peek the type of the current token.
        /// </summary>
        /// <param name="type">The token-type to peek for.</param>
        /// <remarks>
        /// The method returns false if the current token is out-of-bounds.
        /// </remarks>
        /// <returns>True, if the type matches the current token. Otherwise, false.</returns>
        private bool Peek(TokenType type)
        {
            return this.CurrentToken.Type == type;
        }

        /// <summary>
        /// Peek the value of the current token.
        /// </summary>
        /// <param name="value">The value to peek for.</param>
        /// <remarks>
        /// The method returns false if the current token is out-of-bounds.
        /// </remarks>
        /// <returns>True, if the value matches the current token. Otherwise, false.</returns>
        private bool Peek(string value)
        {
            return this.CurrentToken.Value == value;
        }

        /// <summary>
        /// Peek the type of the current token and throw if it doesn't match any of the specified types.
        /// </summary>
        /// <param name="types">The token-types to assert for.</param>
        /// <exception cref="UnexpectedTokenException">Thrown if the assertion fails.</exception>
        private void Expect(params TokenType[] types)
        {
            if(!types.Any(v => this.Peek(v)))
            {
                throw UnexpectedToken();
            }
        }

        /// <summary>
        /// Peek the value of the current token and throw if it doesn't match any of the specified values.
        /// </summary>
        /// <param name="values">The values to assert for.</param>
        /// <exception cref="UnexpectedTokenException">Thrown if the assertion fails.</exception>
        private void Expect(params string[] values)
        {
            if(!values.Any(v => this.Peek(v)))
            {
                throw UnexpectedToken();
            }
        }

        /// <summary>
        /// Progress to the next token.
        /// </summary>
        /// <remarks>
        /// If the position points to the end of the file, nothing is done.
        /// </remarks>
        private void Skip()
        {
            this.CurrentTokenIndex++;
            this.CurrentToken = this.Tokenizer.GetNextToken();
        }

        /// <summary>
        /// Return UnexpectedTokenException-object with location attached as description.
        /// </summary>
        /// <returns>UnexpectedTokenException-object.</returns>
        private Exception UnexpectedToken()
            => new UnexpectedTokenException(this.CurrentToken.Start);

        /// <summary>
        /// <para>
        /// Iterator delegate for the IterateParse-method.
        /// </para>
        /// <para>
        /// The function should continue to return a non-null object,
        /// until the processed buffer is empty, where it should return null.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of object to read.</typeparam>
        /// <returns>The non-null object retrieved, when still processing. Otherwise, null.</returns>
        delegate T? IterateParseFunction<T>();

        /// <summary>
        /// <para>
        /// Read a list of objects until the specified delegate returns null.
        /// </para>
        /// <para>
        /// This method is used to parse tokens at the current token index, until the specified
        /// delegate returns null. The delegate should null, when all tokens in the context
        /// have been parsed and returned.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of object to read.</typeparam>
        /// <returns>A list of objects, of type T, found in the current buffer.</returns>
        private List<T> IterateParse<T>(IterateParseFunction<T> parserFunction) where T : class
        {
            List<T> nodes = new List<T>();

            while(true)
            {
                T? value = null;

                if((value = parserFunction()) != null)
                {
                    nodes.Add(value);
                }
                else
                {
                    break;
                }
            }

            return nodes;
        }

        /// <summary>
        /// Parse the specified source into a GraphDocument-object.
        /// </summary>
        /// <param name="source">The GraphQL schema source.</param>
        /// <param name="options">Options for the parser.</param>
        /// <returns>The parsed GraphDocument-object.</returns>
        /// <exception cref="MissingBraceException">Thrown when the number of braces isn't even.</exception>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphDocument Parse(string source, SchemaParserOptions options)
        {
            this.Tokenizer.SetSource(source);
            this.CurrentToken = this.Tokenizer.GetNextToken();

            this.Document = new GraphDocument();
            this.Document.Source = source;
            this.Options = options;

            this.Document.Definitions = this.IterateParse<GraphDefinition>(this.ParseDefinition);

            if(this.CurrentDepth != 0)
            {
                throw new MissingBraceException();
            }

            return this.Document;
        }

        /// <summary>
        /// Parse the specified source into a GraphDocument-object.
        /// </summary>
        /// <param name="source">The GraphQL schema source.</param>
        /// <returns>The parsed GraphDocument-object.</returns>
        /// <exception cref="MissingBraceException">Thrown when the number of braces isn't even.</exception>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphDocument Parse(string source)
            => this.Parse(source, new SchemaParserOptions());

        /// <summary>
        /// Parse a top-level definition in the GraphQL-document, such operations, schemas, types, etc.
        /// </summary>
        /// <returns>A non-null GraphDefinition, if a definition was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphDefinition? ParseDefinition()
        {
            if(this.Peek(TokenType.BRACE_LEFT))
            {
                return this.ParseOperationDefinition();
            }

            if(this.Peek(TokenType.NAME))
            {
                return this.ParseNamedDefinition();
            }

            if(this.Peek(TokenType.STRING))
            {
                return this.ParseNamedDefinitionWithDescription();
            }

            return null;
        }

        /// <summary>
        /// Parse a named top-level definition in the GraphQL-document, such operations, schemas, types, etc.
        /// </summary>
        /// <returns>A non-null GraphDefinition, if a definition was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphDefinition? ParseNamedDefinition()
        {
            return this.CurrentToken.Value switch
            {
                "{"             => this.ParseOperationDefinition(),
                "query"         => this.ParseOperationDefinition(),
                "mutation"      => this.ParseOperationDefinition(),
                "subscription"  => this.ParseOperationDefinition(),
                "interface"     => this.ParseInterfaceDefinition(),
                "type"          => this.ParseObjectTypeDefinition(),
                "input"         => this.ParseInputTypeDefinition(),
                "scalar"        => this.ParseScalarTypeDefinition(),
                "schema"        => this.ParseSchemaDefinition(),
                "extend"        => this.ParseExtensionDefinition(),
                "enum"          => this.ParseEnumDefinition(),
                "union"         => this.ParseUnionDefinition(),
                "fragment"      => this.ParseFragmentDefinition(),
                "directive"     => this.ParseDirectiveDefinition(),

                _ => throw UnexpectedToken()
            };
        }
    
        /// <summary>
        /// Parse a named top-level definition with a description in the GraphQL-document, such operations, schemas, types, etc.
        /// </summary>
        /// <returns>A non-null GraphDefinition, if a definition was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphDefinition? ParseNamedDefinitionWithDescription()
        {
            GraphDescription? description = this.ParseDescription();
            GraphDefinition? def = this.ParseNamedDefinition();

            if(def != null)
            {
                def.Description = description;
            }

            return def;
        }
    }
}
using Chart.Language.SyntaxTree;

namespace Chart.Language
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
        internal SchemaParserOptions Options = new();

        /// <summary>
        /// The source document.
        /// </summary>
        internal Tokenizer Tokenizer = new();

        /// <summary>
        /// The parent document for the parsing.
        /// </summary>
        internal GraphDocument Document { get; set; } = default!;

        /// <summary>
        /// The current position into the token-list
        /// </summary>
        internal int CurrentTokenIndex { get; set; } = 0;

        /// <summary>
        /// The currently-processing token
        /// </summary>
        internal Token CurrentToken = new();

        /// <summary>
        /// Peek the type of the current token.
        /// </summary>
        /// <param name="type">The token-type to peek for.</param>
        /// <remarks>
        /// The method returns false if the current token is out-of-bounds.
        /// </remarks>
        /// <returns>True, if the type matches the current token. Otherwise, false.</returns>
        internal bool Peek(TokenType type)
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
        internal bool Peek(string value)
        {
            return this.CurrentToken.Value == value;
        }

        /// <summary>
        /// Peek the type of the current token and throw if it doesn't match any of the specified types.
        /// </summary>
        /// <param name="types">The token-types to assert for.</param>
        /// <exception cref="UnexpectedTokenException">Thrown if the assertion fails.</exception>
        internal void Expect(params TokenType[] types)
        {
            if(!types.Any(v => this.Peek(v)))
            {
                throw this.UnexpectedToken();
            }
        }

        /// <summary>
        /// Peek the value of the current token and throw if it doesn't match any of the specified values.
        /// </summary>
        /// <param name="values">The values to assert for.</param>
        /// <exception cref="UnexpectedTokenException">Thrown if the assertion fails.</exception>
        internal void Expect(params string[] values)
        {
            if(!values.Any(v => this.Peek(v)))
            {
                throw this.UnexpectedToken();
            }
        }

        /// <summary>
        /// Progress to the next token.
        /// </summary>
        /// <remarks>
        /// If the position points to the end of the file, nothing is done.
        /// </remarks>
        internal void Skip()
        {
            this.CurrentTokenIndex++;
            this.CurrentToken = this.Tokenizer.GetNextToken();
        }

        /// <summary>
        /// Return UnexpectedTokenException-object with location attached as description.
        /// </summary>
        /// <returns>UnexpectedTokenException-object.</returns>
        internal Exception UnexpectedToken()
            => new UnexpectedTokenException(this.CurrentToken);

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
        internal delegate T? IterateParseFunction<T>();

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
        internal List<T> IterateParse<T>(IterateParseFunction<T> parserFunction) where T : class
        {
            List<T> nodes = new();

            while(true)
            {
                T? value;
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
        /// Parse the specified source into a <see cref="GraphDocument" />-object.
        /// </summary>
        /// <param name="source">The GraphQL schema source.</param>
        /// <param name="executable">Whether the parser should parse an executable query or schema.</param>
        /// <param name="options">Options for the parser.</param>
        /// <returns>The parsed <see cref="GraphDocument" />-object.</returns>
        /// <exception cref="MissingBraceException">Thrown when the number of braces isn't even.</exception>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphDocument Parse(string source, bool executable, SchemaParserOptions? options = null)
        {
            this.Tokenizer.SetSource(source);
            this.CurrentToken = this.Tokenizer.GetNextToken();

            while(this.CurrentToken.Type == TokenType.COMMENT)
            {
                this.CurrentToken = this.Tokenizer.GetNextToken();
            }

            this.Document = new() { Source = source };
            this.Options = options ?? new SchemaParserOptions();

            this.Document.Definitions = this.IterateParse<GraphDefinition>(() => this.ParseDefinition(executable));

            return this.Document;
        }

        /// <summary>
        /// Parse the specified schema source into a <see cref="GraphDocument" />-object.
        /// </summary>
        /// <inheritdoc cref="SchemaParser.Parse(string, bool, SchemaParserOptions?)" />
        public GraphDocument ParseSchema(string source, SchemaParserOptions? options = null)
            => this.Parse(source, false, options);

        /// <summary>
        /// Parse the specified query into a <see cref="GraphDocument" />-object.
        /// </summary>
        /// <inheritdoc cref="SchemaParser.Parse(string, bool, SchemaParserOptions?)" />
        public GraphDocument ParseQuery(string source, SchemaParserOptions? options = null)
            => this.Parse(source, true, options);

        /// <summary>
        /// Parse a top-level definition in the GraphQL-document, such operations, schemas, types, etc.
        /// </summary>
        /// <param name="executable">Whether the parser should parse an executable query or schema.</param>
        /// <returns>A non-null GraphDefinition, if a definition was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphDefinition? ParseDefinition(bool executable) =>
            this.CurrentToken.Type switch
            {
                TokenType.BRACE_LEFT => this.ParseNamedDefinition(executable),
                TokenType.NAME => this.ParseNamedDefinition(executable),
                TokenType.STRING => this.ParseNamedDefinitionWithDescription(executable),
                _ => null,
            };

        /// <summary>
        /// Parse a named top-level definition in the GraphQL-document, such operations, schemas, types, etc.
        /// </summary>
        /// <returns>A non-null GraphDefinition, if a definition was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphDefinition? ParseNamedDefinition(bool executable) =>
            executable switch
            {
                true => this.CurrentToken.Value switch
                {
                    "{" => this.ParseOperationDefinition(),
                    "query" => this.ParseOperationDefinition(),
                    "mutation" => this.ParseOperationDefinition(),
                    "subscription" => this.ParseOperationDefinition(),
                    "fragment" => this.ParseFragmentDefinition(),

                    string v => throw new UnexpectedTokenException(v, this.CurrentToken.Start)
                },
                false => this.CurrentToken.Value switch
                {
                    "interface" => this.ParseInterfaceDefinition(),
                    "type" => this.ParseObjectTypeDefinition(),
                    "input" => this.ParseInputTypeDefinition(),
                    "scalar" => this.ParseScalarTypeDefinition(),
                    "schema" => this.ParseSchemaDefinition(),
                    "extend" => this.ParseExtensionDefinition(),
                    "enum" => this.ParseEnumDefinition(),
                    "union" => this.ParseUnionDefinition(),
                    "directive" => this.ParseDirectiveDefinition(),

                    string v => throw new UnexpectedTokenException(v, this.CurrentToken.Start)
                }
            };

        /// <summary>
        /// Parse a named top-level definition with a description in the GraphQL-document, such operations, schemas, types, etc.
        /// </summary>
        /// <returns>A non-null GraphDefinition, if a definition was found. Otherwise, null.</returns>
        /// <exception cref="UnexpectedTokenException">Thrown when an unexpected token was found.</exception>
        public GraphDefinition? ParseNamedDefinitionWithDescription(bool executable)
        {
            GraphDescription? description = this.ParseDescription();
            GraphDefinition? def = this.ParseNamedDefinition(executable);

            if(def != null)
            {
                def.Description = description;
            }

            return def;
        }

        public TDefinition ParseString<TDefinition>(string value, Func<SchemaParser, TDefinition> action)
            where TDefinition : IGraphNode
        {
            this.Tokenizer.SetSource(value);
            this.CurrentToken = this.Tokenizer.GetNextToken();

            return action(this);
        }
    }
}
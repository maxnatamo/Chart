using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public class Tokenizer
    {
        /// <summary>
        /// List of punctuation tokens
        /// </summary>
        private static readonly char[] PUNCTUATION_TOKENS =
        {
            ':',
            '[',
            ']',
            '{',
            '}',
            '(',
            ')'
        };

        /// <summary>
        /// List of whitespace tokens
        /// </summary>
        private static readonly char[] WHITESPACE_TOKENS =
        {
            '\r',
            '\t',
            '\n',
            ',',
            ' '
        };

        /// <summary>
        /// The source document.
        /// </summary>
        private string Source = string.Empty;

        /// <summary>
        /// The position into the document currently being processed.
        /// </summary>
        private int CurrentIndex = 0;

        /// <summary>
        /// The current location of the document currently being processed.
        /// </summary>
        private readonly GraphLocation CurrentLocation = new();

        /// <summary>
        /// Initialize a new tokenizer without a GraphQL-document attached.
        /// </summary>
        public Tokenizer()
        {
            this.CurrentIndex = 0;
        }

        /// <summary>
        /// Initialize a new tokenizer with a GraphQL-document.
        /// </summary>
        /// <param name="source">The GraphQL-document to use.</param>
        public Tokenizer(string source)
        {
            this.SetSource(source);
        }

        public void SetSource(string source)
        {
            this.Source = source;
            this.CurrentIndex = 0;
        }

        /// <summary>
        /// Get the next token in the document.
        /// </summary>
        /// <returns>The next token parsed.</returns>
        public Token GetNextToken()
        {
            int start = this.CurrentIndex;
            Token token = this.ReadToken();

            // Ignore comments
            while(token.Type == TokenType.COMMENT)
            {
                this.CurrentIndex = token.End;
                token = this.ReadToken();
            }

            this.GetLocation(token, start);

            // Both line and column are to start from 1.
            // Source: sec-Errors.Error-result-format
            token.Location.Line = this.CurrentLocation.Line + 1;
            token.Location.Column = this.CurrentLocation.Column + 1;

            this.CurrentIndex = token.End;
            return token;
        }

        /// <summary>
        /// Get all tokens in the current tokenizer source.
        /// </summary>
        public List<Token> GetAllTokens()
        {
            List<Token> tokens = new List<Token>();

            this.CurrentIndex = 0;

            Token token;
            while((token = this.GetNextToken()).Type != TokenType.EOF)
            {
                tokens.Add(token);
            }

            return tokens;
        }

        /// <summary>
        /// Get the location of the given token. The result is set at <see cref="Tokenizer.CurrentLocation" />.
        /// </summary>
        /// <param name="token">The token to get the location from.</param>
        /// <param name="prevIndex">The location index before parsing the token.</param>
        /// <example>
        /// <code>
        /// int prevIndex = this.CurrentIndex;
        /// Token token = this.ReadToken();
        ///
        /// this.GetLocation(token, prevIndex);
        /// </code>
        /// </example>
        private void GetLocation(Token token, int prevIndex)
        {
            string contentSinceLast = this.Source.Substring(prevIndex, token.End - prevIndex);
            int newlines = contentSinceLast.Count(v => v == '\n');

            this.CurrentLocation.Line += newlines;
            this.CurrentLocation.Column = 0;

            for(int i = token.Start - 1; i >= 0; i--)
            {
                if(this.Source[i] == '\n')
                {
                    break;
                }

                this.CurrentLocation.Column++;
            }

            token.Location.Line = this.CurrentLocation.Line;
            token.Location.Column = this.CurrentLocation.Column;
        }

        /// <summary>
        /// Read the token at the current position and return it.
        /// </summary>
        /// <returns>The parsed token.</returns>
        private Token ReadToken()
        {
            string? match = "";

            if(string.IsNullOrEmpty(this.Source))
            {
                return new Token(TokenType.EOF, 0, 0);
            }

            this.MoveToNextWord();

            if(this.CurrentIndex >= this.Source.Length)
            {
                return new Token(TokenType.EOF, this.CurrentIndex, this.CurrentIndex);
            }

            char firstChar = this.Source[this.CurrentIndex];

            if(firstChar < ' ' && firstChar != '\t' && firstChar != '\n')
            {
                throw new InvalidDataException($"Invalid token {(int) firstChar}");
            }

            if(PUNCTUATION_TOKENS.Contains(firstChar))
            {
                return this.ParsePunctuationToken();
            }

            if(this.ContainsNext("null"))
            {
                return new Token(TokenType.NULL, this.CurrentIndex, this.CurrentIndex + 4);
            }

            if(this.ContainsNext("...", false))
            {
                return new Token(TokenType.SPREAD, this.CurrentIndex, this.CurrentIndex + 3);
            }

            if(this.ContainsNext(out match, new string[] { "true", "false" }))
            {
                return new Token(TokenType.BOOLEAN, match, this.CurrentIndex, this.CurrentIndex + match.Length);
            }

            if(firstChar == '#')
            {
                return this.ParseCommentToken();
            }

            if(firstChar == '$')
            {
                return new Token(TokenType.DOLLAR_SIGN, "$", this.CurrentIndex, this.CurrentIndex + 1);
            }

            if(firstChar == '|')
            {
                return new Token(TokenType.PIPE, "|", this.CurrentIndex, this.CurrentIndex + 1);
            }

            if(firstChar == '=')
            {
                return new Token(TokenType.EQUAL, "=", this.CurrentIndex, this.CurrentIndex + 1);
            }

            if(firstChar == '!')
            {
                return new Token(TokenType.EXCLAMATION_POINT, "!", this.CurrentIndex, this.CurrentIndex + 1);
            }

            if(firstChar == '@')
            {
                return new Token(TokenType.AT, "@", this.CurrentIndex, this.CurrentIndex + 1);
            }

            if(firstChar == '&')
            {
                return new Token(TokenType.AMPERSAND, "&", this.CurrentIndex, this.CurrentIndex + 1);
            }

            if(('a' <= firstChar && firstChar <= 'z') || ('A' <= firstChar && firstChar <= 'Z') || firstChar == '_')
            {
                return this.ParseNameToken();
            }

            if(('0' <= firstChar && firstChar <= '9') || firstChar == '-')
            {
                return this.ParseNumberToken();
            }

            if(this.ContainsNext("\"\"\"", false))
            {
                return this.ParseBlockStringToken();
            }

            if(firstChar == '"')
            {
                return this.ParseStringToken();
            }

            return new Token(TokenType.UNKNOWN, firstChar.ToString(), this.CurrentIndex, this.CurrentIndex + 1);
        }

        /// <summary>
        /// Move the position to the start of the next word.
        /// </summary>
        private void MoveToNextWord()
        {
            int pos = this.CurrentIndex;

            while(pos < this.Source.Length)
            {
                if(WHITESPACE_TOKENS.Contains(this.Source[pos]))
                {
                    pos++;
                }
                else
                {
                    this.CurrentIndex = pos;
                    return;
                }
            }

            this.CurrentIndex = pos;
        }

        /// <summary>
        /// Try to parse a punctuation-token.
        /// </summary>
        /// <returns>A valid token, if successful. Otherwise, Token of UNKNOWN-type.</returns>
        private Token ParsePunctuationToken() =>
            this.Source[this.CurrentIndex] switch
            {
                ':' => new Token(TokenType.COLON, ":", this.CurrentIndex, this.CurrentIndex + 1),
                '(' => new Token(TokenType.PARENTHESIS_LEFT, "(", this.CurrentIndex, this.CurrentIndex + 1),
                ')' => new Token(TokenType.PARENTHESIS_RIGHT, ")", this.CurrentIndex, this.CurrentIndex + 1),
                '[' => new Token(TokenType.BRACKET_LEFT, "[", this.CurrentIndex, this.CurrentIndex + 1),
                ']' => new Token(TokenType.BRACKET_RIGHT, "]", this.CurrentIndex, this.CurrentIndex + 1),
                '{' => new Token(TokenType.BRACE_LEFT, "{", this.CurrentIndex, this.CurrentIndex + 1),
                '}' => new Token(TokenType.BRACE_RIGHT, "}", this.CurrentIndex, this.CurrentIndex + 1),

                _ => new Token(TokenType.UNKNOWN, this.CurrentIndex, this.CurrentIndex + 1)
            };

        /// <summary>
        /// Parse a comment on the current position.
        /// </summary>
        /// <returns>A valid comment token.</returns>
        internal Token ParseCommentToken()
        {
            string value = this.GetUntil(c => (c != '\n' && c != '\r'));

            // Remove hashtag (#)
            string trimmed = value.TrimStart('#').Trim();

            return new Token(TokenType.COMMENT, trimmed, this.CurrentIndex, this.CurrentIndex + value.Length);
        }

        /// <summary>
        /// Parse a name token on the current position.
        /// </summary>
        /// <returns>A valid name token.</returns>
        internal Token ParseNameToken()
        {
            string name = this.GetUntil(c
                => ('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z') || ('0' <= c && c <= '9') || c == '_');

            return new Token(TokenType.NAME, name, this.CurrentIndex, this.CurrentIndex + name.Length);
        }

        /// <summary>
        /// Parse a number token on the current position.
        /// </summary>
        /// <returns>A valid float- or int-token.</returns>
        internal Token ParseNumberToken()
        {
            NumberStyles styles = NumberStyles.Float;

            string stringValue = this.GetUntil(c => (('0' <= c && c <= '9') || c == '.' || c == '-' || c == 'E' || c == 'e'));
            int length = stringValue.Length;

            if(Int32.TryParse(stringValue, NumberStyles.AllowLeadingSign, null, out Int32 intValue))
            {
                return new Token(TokenType.INT, intValue.ToString(), this.CurrentIndex, this.CurrentIndex + length);
            }

            if(Double.TryParse(stringValue, styles, null, out Double floatValue))
            {
                return new Token(TokenType.FLOAT, floatValue.ToString(), this.CurrentIndex, this.CurrentIndex + length);
            }

            return new Token(TokenType.UNKNOWN, stringValue, this.CurrentIndex, this.CurrentIndex + length);
        }

        /// <summary>
        /// Parse a string token on the current position.
        /// </summary>
        /// <remarks>
        /// Quotes are not included in the value.
        /// </remarks>
        /// <returns>A valid string token.</returns>
        internal Token ParseStringToken()
        {
            string value = this.GetUntil(c => (c >= ' ' && c != '"'));
            string trimmed = value.Substring(1, value.Length - 1).Trim();

            return new Token(TokenType.STRING, trimmed, this.CurrentIndex, this.CurrentIndex + value.Length + 1);
        }

        /// <summary>
        /// Parse a block string token on the current position.
        /// </summary>
        /// <remarks>
        /// Quotes are not included in the value.
        /// </remarks>
        /// <returns>A valid string token.</returns>
        internal Token ParseBlockStringToken()
        {
            string value = this.GetUntil(str => !str.StartsWith("\"\"\""));
            string trimmed = value.Substring(3, value.Length - 3);

            trimmed = this.RemoveCommonIndentation(trimmed).Trim();

            return new Token(TokenType.STRING, trimmed, this.CurrentIndex, this.CurrentIndex + value.Length + 3);
        }

        /// <summary>
        /// Get string of following content, until the predicate fails or until source ends.
        /// </summary>
        /// <param name="predicate">Retrieve characters until this predicate fails.</param>
        /// <returns>The next content in the source.</returns>
        internal string GetUntil(Func<char, bool> predicate)
        {
            char c;
            int idx = this.CurrentIndex;

            do
            {
                idx++;

                if(idx < this.Source.Length)
                {
                    c = this.Source[idx];
                }
                else
                {
                    break;
                }
            }
            while(predicate(c));

            int length = idx - this.CurrentIndex;

            return this.Source.Substring(this.CurrentIndex, length);
        }

        /// <summary>
        /// Get string of following content, until the predicate fails or until source ends.
        /// </summary>
        /// <param name="predicate">Retrieve characters until this predicate fails.</param>
        /// <param name="windowSize">Size of the search window, to limit performance impact.</param>
        /// <returns>The next content in the source.</returns>
        internal string GetUntil(Func<string, bool> predicate, int windowSize = 3)
        {
            string window = "";
            int idx = this.CurrentIndex;

            do
            {
                idx++;

                if(idx < this.Source.Length)
                {
                    // Avoid ArgumentOutOfRangeException, when startIndex + length
                    // is more than the length of the source string.
                    int length = Math.Min(windowSize, this.Source.Length - idx);

                    window = this.Source.Substring(idx, length);
                }
                else
                {
                    break;
                }
            }
            while(predicate(window));

            Span<char> buffer = stackalloc char[idx - this.CurrentIndex];

            for(int i = 0; i < (idx - this.CurrentIndex); i++)
            {
                buffer[i] = this.Source[this.CurrentIndex + i];
            }

            return new string(buffer);
        }

        /// <summary>
        /// Check whether the specified string is present as the next string.
        /// </summary>
        /// <param name="next">The string to check for in the source.</param>
        /// <param name="matchWhole">Whether to match the whole word or not.</param>
        /// <returns>True, if the string is next in the source. Otherwise, false.</returns>
        internal bool ContainsNext(string next, bool matchWhole = true)
        {
            if(this.CurrentIndex + next.Length > this.Source.Length)
            {
                return false;
            }

            for(int i = 0; i < next.Length; i++)
            {
                if(this.Source[this.CurrentIndex + i] != next[i])
                {
                    return false;
                }
            }

            // We should only return true when the whole word is match,
            // not just the beginning of the word.
            if(matchWhole && this.CurrentIndex + next.Length < this.Source.Length)
            {
                char charAfterString = this.Source[this.CurrentIndex + next.Length];

                if(!WHITESPACE_TOKENS.Contains(charAfterString) && !PUNCTUATION_TOKENS.Contains(charAfterString))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check whether any of the specified strings is present as the next string.
        /// </summary>
        /// <param name="next">List of strings to check for in the source.</param>
        /// <param name="matchWhole">Whether to match the whole word or not.</param>
        /// <returns>True, if any of the inputs is next in the source. Otherwise, false.</returns>
        internal bool ContainsNext([NotNullWhen(true)] out string? match, string[] next, bool matchWhole = true)
        {
            match = next.FirstOrDefault(v => this.ContainsNext(v, matchWhole));
            return match is not null;
        }

        /// <summary>
        /// Remove common indentation for block quotes.
        /// </summary>
        /// <param name="blockString">The block quote to remove indentation for.</param>
        /// <returns>The formatted block quote.</returns>
        /// <seealso href="https://spec.graphql.org/October2021/#BlockStringValue()">Stand-in method.</seealso>
        internal string RemoveCommonIndentation(string blockString)
        {
            int minIndent = int.MaxValue;
            string[] lines = blockString.Split('\n');

            for(int i = 0; i < lines.Length; i++)
            {
                // Skip empty files
                if(string.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }

                int indent = lines[i].IndexNotOfAny(WHITESPACE_TOKENS);
                if(indent == -1)
                {
                    continue;
                }

                if(minIndent > indent)
                {
                    minIndent = indent;
                }
            }

            if(minIndent <= 0)
            {
                return blockString;
            }

            for(int i = 0; i < lines.Length; i++)
            {
                if(lines[i].Length <= minIndent)
                {
                    continue;
                }

                lines[i] = lines[i].Substring(minIndent);
            }

            return string.Join(System.Environment.NewLine, lines);
        }
    }
}
using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public readonly struct Token
    {
        public readonly TokenType Type;

        public readonly string Value;

        public readonly int Start;

        public readonly int End;

        public readonly GraphLocation Location = new GraphLocation();

        public Token(TokenType type, string value, int start, int end)
        {
            this.Type = type;
            this.Value = value;
            this.Start = start;
            this.End = end;
        }

        public Token(TokenType type, int start, int end)
        {
            this.Type = type;
            this.Value = string.Empty;
            this.Start = start;
            this.End = end;
        }

        public override string ToString() => this.HasTypeValue()
            ? $"{this.TypeDescription()} => \"{this.Value}\""
            : $"{this.TypeDescription()}";

        private bool HasTypeValue() =>
            this.Type == TokenType.NAME ||
            this.Type == TokenType.INT ||
            this.Type == TokenType.FLOAT ||
            this.Type == TokenType.STRING ||
            this.Type == TokenType.BOOLEAN ||
            this.Type == TokenType.COMMENT ||
            this.Type == TokenType.UNKNOWN;

        private string TypeDescription() => this.Type switch
        {
            TokenType.EOF => "EOF",
            TokenType.EXCLAMATION_POINT => "!",
            TokenType.DOLLAR_SIGN => "$",
            TokenType.AT => "@",
            TokenType.PIPE => "|",
            TokenType.AMPERSAND => "&",
            TokenType.EQUAL => "=",
            TokenType.PARENTHESIS_LEFT => "(",
            TokenType.PARENTHESIS_RIGHT => ")",
            TokenType.BRACKET_LEFT => "[",
            TokenType.BRACKET_RIGHT => "]",
            TokenType.BRACE_LEFT => "{",
            TokenType.BRACE_RIGHT => "}",
            TokenType.COLON => ":",
            TokenType.SPREAD => "...",
            TokenType.NAME => "Name",
            TokenType.INT => "Int",
            TokenType.FLOAT => "Float",
            TokenType.STRING => "String",
            TokenType.BOOLEAN => "Bool",
            TokenType.NULL => "Null",
            TokenType.COMMENT => "Comment",
            TokenType.UNKNOWN => "Unknown",

            _ => throw new InvalidDataException(this.Type.ToString())
        };
    }
}
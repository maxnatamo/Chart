namespace Chart.Language
{
    public enum TokenType
    {
        /// <summary>
        /// End-of-line
        /// </summary>
        EOF,

        /// <summary>
        /// Exclamation pointer, !
        /// </summary>
        EXCLAMATION_POINT,

        /// <summary>
        /// Dollar sign, $
        /// </summary>
        DOLLAR_SIGN,

        /// <summary>
        /// At-symbol, @
        /// </summary>
        AT,

        /// <summary>
        /// Pipe-symbol, |
        /// </summary>
        PIPE,

        /// <summary>
        /// Ampersand-symbol, &#38;
        /// </summary>
        AMPERSAND,

        /// <summary>
        /// Equal sign, =
        /// </summary>
        EQUAL,

        /// <summary>
        /// Open parenthesis, (
        /// </summary>
        PARENTHESIS_LEFT,

        /// <summary>
        /// Close parenthesis, )
        /// </summary>
        PARENTHESIS_RIGHT,

        /// <summary>
        /// Open parenthesis, [
        /// </summary>
        BRACKET_LEFT,

        /// <summary>
        /// Close parenthesis, ]
        /// </summary>
        BRACKET_RIGHT,

        /// <summary>
        /// Open brace, {
        /// </summary>
        BRACE_LEFT,

        /// <summary>
        /// Close brace, }
        /// </summary>
        BRACE_RIGHT,

        /// <summary>
        /// Colon, :
        /// </summary>
        COLON,

        /// <summary>
        /// Fragment spread, ...
        /// </summary>
        SPREAD,

        /// <summary>
        /// Named field in the document.
        /// </summary>
        NAME,

        /// <summary>
        /// A signed integer value in the document.
        /// </summary>
        INT,

        /// <summary>
        /// A floating-point number in the document.
        /// </summary>
        FLOAT,

        /// <summary>
        /// A string value in the document.
        /// </summary>
        STRING,

        /// <summary>
        /// A boolean value in the document. Either true or false, case-sensitive.
        /// </summary>
        BOOLEAN,

        /// <summary>
        /// An undefined and/or null value in the document.
        /// </summary>
        NULL,

        /// <summary>
        /// A comment inside the document.
        /// </summary>
        COMMENT,

        /// <summary>
        /// An unidentified value in the document.
        /// </summary>
        UNKNOWN,
    }
}
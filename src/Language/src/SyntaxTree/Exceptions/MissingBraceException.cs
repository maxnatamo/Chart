namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Should be thrown when a brace was expected, but none was found.
    /// </summary>
    public class MissingBraceException : Exception
    {
        /// <summary>
        /// Initialize new MissingBraceException-object.
        /// </summary>
        public MissingBraceException()
            : base($"The depth was not zero after parsing the document.")
        { }
    }
}
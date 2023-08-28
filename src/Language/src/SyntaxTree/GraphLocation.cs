namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Location of an ASTNode.
    /// </summary>
    public class GraphLocation
    {
        /// <summary>
        /// The line index of the node into the original source, braces included.
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// The character index of the node, relative to the start of the line.
        /// </summary>
        public int Column { get; set; }

        public GraphLocation()
        {
            this.Line = 0;
            this.Column = 0;
        }

        public GraphLocation(int line, int column)
        {
            this.Line = line;
            this.Column = column;
        }

        public GraphLocation Clone() =>
            new GraphLocation(this.Line, this.Column);
    }
}
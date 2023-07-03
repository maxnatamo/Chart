namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Location of an ASTNode.
    /// </summary>
    public class GraphLocation
    {
        /// <summary>
        /// The start index of the node into the original source, braces included.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// The end index of the node into the original source, braces included.
        /// </summary>
        public int End { get; set; }

        public GraphLocation()
        {
            this.Start = 0;
            this.End = 0;
        }

        public GraphLocation(int start, int end)
        {
            this.Start = start;
            this.End = end;
        }
    }
}
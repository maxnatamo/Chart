namespace Chart.Language.SyntaxTree
{
    public interface IHasDescription
    {
        /// <summary>
        /// An optional description.
        /// </summary>
        public GraphDescription? Description { get; set; }
    }
}
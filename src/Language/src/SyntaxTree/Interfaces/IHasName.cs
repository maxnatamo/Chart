namespace Chart.Language.SyntaxTree
{
    public interface IHasName
    {
        /// <summary>
        /// The name of the definition.
        /// </summary>
        public GraphName Name { get; set; }
    }
}
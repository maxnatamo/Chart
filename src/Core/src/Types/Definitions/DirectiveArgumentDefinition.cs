using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    /// <summary>
    /// An instance of an argument value.
    /// </summary>
    public class DirectiveArgumentDefinition : TypeDefinition, ICreateDefinition<GraphArgument>
    {
        /// <inheritdoc />
        public override string Name { get; protected set; }

        /// <summary>
        /// The value of the argument.
        /// </summary>
        public IGraphValue Value { get; set; }

        public DirectiveArgumentDefinition(string name, IGraphValue value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <inheritdoc />
        public GraphArgument CreateSyntaxNode(IServiceProvider services) =>
            new()
            {
                Name = new GraphName(this.Name),
                Value = this.Value,
            };
    }
}
using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class Directive : TypeDefinition, ICreateDefinition<GraphDirective>
    {
        /// <inheritdoc />
        public override string Name { get; protected set; }

        /// <summary>
        /// List of arguments for the directive.
        /// </summary>
        public readonly List<DirectiveArgumentDefinition> Arguments = new();

        /// <summary>
        /// Whether the directive has any Arguments.
        /// </summary>
        public bool HasArguments => this.Arguments is { Count: > 0 };

        public Directive(string name)
        {
            this.Name = name;
        }

        /// <inheritdoc />
        public GraphDirective CreateSyntaxNode(IServiceProvider services) =>
            new()
            {
                Name = new GraphName(this.Name),
                Arguments = new GraphArguments
                {
                    Arguments = this.Arguments
                        .Select(v => v.CreateSyntaxNode(services))
                        .ToList()
                }
            };
    }
}
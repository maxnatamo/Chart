using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class DirectiveDefinition : TypeDefinition, ICreateDefinition<GraphDirectiveDefinition>
    {
        /// <inheritdoc />
        public override string Name { get; protected set; }

        /// <summary>
        /// Possible locations which the directive can be applied on.
        /// </summary>
        public GraphDirectiveLocation Locations { get; protected set; }

        /// <summary>
        /// Whether this directive is repeatable.
        /// </summary>
        public bool Repeatable { get; protected set; } = false;

        /// <summary>
        /// List of arguments available on the directive.
        /// </summary>
        public readonly List<ArgumentDefinition> Arguments = new();

        /// <summary>
        /// Whether the directive has any Arguments.
        /// </summary>
        public bool HasArguments => this.Arguments is { Count: > 0 };

        public DirectiveDefinition(string name, GraphDirectiveLocation locations)
        {
            this.Name = name;
            this.Locations = locations;
        }

        /// <summary>
        /// Executed for each definition the directive is set on, during execution.
        /// </summary>
        public virtual void OnExecute()
        { }

        /// <inheritdoc />
        public GraphDirectiveDefinition CreateSyntaxNode(IServiceProvider services) =>
            new()
            {
                Name = new GraphName(this.Name),
                Description = GraphDescription.From(this.Description),
                Repeatable = this.Repeatable,
                Locations = new()
                {
                    Locations = this.Locations
                },
                Arguments = new GraphArgumentsDefinition
                {
                    Arguments = this.Arguments
                        .Select(v => v.CreateSyntaxNode(services))
                        .ToList()
                }
            };
    }
}
using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class EnumValueType : TypeDefinition, ICreateDefinition<GraphEnumDefinitionValue>
    {
        /// <summary>
        /// The name of the enum type, which will be used in the schema.
        /// </summary>
        public override string Name { get; protected set; }

        /// <summary>
        /// List of directives applied onto the type.
        /// </summary>
        public readonly List<Directive> Directives = new();

        /// <summary>
        /// Whether the type has any directives.
        /// </summary>
        public bool HasDirectives => this.Directives is { Count: > 0 };

        public EnumValueType(
            string name,
            string? description = null,
            Type? runtimeType = null,
            List<Directive>? directives = null)
        {
            this.Name = name;
            this.Description = description;
            this.RuntimeType = runtimeType;

            if(directives is not null)
            {
                this.Directives.AddRange(directives);
            }
        }

        public GraphEnumDefinitionValue CreateSyntaxNode(IServiceProvider services) =>
            new()
            {
                Name = new GraphName(this.Name),
                Description = GraphDescription.From(this.Description),
                Directives = new GraphDirectives
                {
                    Directives = this.Directives
                        .Select(v => v.CreateSyntaxNode(services))
                        .ToList()
                }
            };
    }
}
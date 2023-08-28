using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class InputObjectType : TypeDefinition, ICreateDefinition<GraphInputDefinition>
    {
        /// <inheritdoc />
        public override string Name { get; protected set; }

        /// <summary>
        /// List of directives applied onto the type.
        /// </summary>
        public readonly List<Directive> Directives = new();

        /// <summary>
        /// Whether the type has any directives.
        /// </summary>
        public bool HasDirectives => this.Directives is { Count: > 0 };

        /// <summary>
        /// List of fields available on the input type.
        /// </summary>
        public readonly List<InputFieldDefinition> Fields = new();

        /// <summary>
        /// Whether the type has any fields.
        /// </summary>
        public bool HasFields => this.Fields is { Count: > 0 };

        public InputObjectType(string name, string? description = null, Type? runtimeType = null)
        {
            this.Name = name;
            this.Description = description;
            this.RuntimeType = runtimeType;
        }

        public GraphInputDefinition CreateSyntaxNode(IServiceProvider services) =>
            new()
            {
                Name = new GraphName(this.Name),
                Description = GraphDescription.From(this.Description),
                Directives = new GraphDirectives
                {
                    Directives = this.Directives
                        .Select(v => v.CreateSyntaxNode(services))
                        .ToList()
                },
                Fields = new GraphInputFieldsDefinition
                {
                    Arguments = this.Fields
                        .Select(v => v.CreateSyntaxNode(services))
                        .ToList()
                }
            };
    }
}
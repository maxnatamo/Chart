using System.Reflection;

using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class FieldDefinition : TypeDefinition, ICreateDefinition<GraphField>
    {
        /// <inheritdoc />
        public override string Name { get; protected set; }

        /// <summary>
        /// The type that the field returns.
        /// </summary>
        public GraphType Type { get; protected set; }

        /// <summary>
        /// The associated member, if any.
        /// </summary>
        protected internal MemberInfo? Member { get; set; } = null;

        /// <summary>
        /// The return type of <see cref="Member" />, if it is set.
        /// </summary>
        protected internal Type? MemberType { get; set; } = null;

        /// <summary>
        /// The runtime type of the parent object.
        /// </summary>
        protected internal Type? ParentType { get; set; } = null;

        /// <summary>
        /// List of arguments available on the directive.
        /// </summary>
        public readonly List<ArgumentDefinition> Arguments = new();

        /// <summary>
        /// Whether the directive has any Arguments.
        /// </summary>
        public bool HasArguments => this.Arguments is { Count: > 0 };

        /// <summary>
        /// List of directives applied onto the field.
        /// </summary>
        public readonly List<Directive> Directives = new();

        /// <summary>
        /// Whether the field has any directives.
        /// </summary>
        public bool HasDirectives => this.Directives is { Count: > 0 };

        /// <summary>
        /// Resolver for retrieving the value from an object.
        /// </summary>
        public FieldResolverDelegate? Resolver { get; set; } = null;

        public FieldDefinition(
            string name,
            GraphType type,
            List<Directive>? directives = null)
        {
            this.Name = name;
            this.Type = type;

            if(directives is not null)
            {
                this.Directives.AddRange(directives);
            }
        }

        public virtual GraphField CreateSyntaxNode(IServiceProvider services) =>
            new()
            {
                Name = new GraphName(this.Name),
                Description = GraphDescription.From(this.Description),
                Type = this.Type,
                Directives = new GraphDirectives
                {
                    Directives = this.Directives
                        .Select(x => x.CreateSyntaxNode(services))
                        .ToList()
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
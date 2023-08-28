using Chart.Language.SyntaxTree;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core
{
    /// <summary>
    /// Definition of a single argument.
    /// </summary>
    /// <example>
    /// The following example demonstrates how to create a new argument, with type of <c>String!</c>:
    /// <code>
    /// ArgumentDefinition argument = new ArgumentDefinition("reason", typeof(NonNullType&#60;StringType&#62;));
    /// </code>
    /// </example>
    public class ArgumentDefinition : TypeDefinition, ICreateDefinition<GraphArgumentDefinition>
    {
        /// <inheritdoc />
        public override string Name { get; protected set; }

        /// <summary>
        /// The type that the argument accepts. Must be a subclass of <see cref="ITypeDefinition" />.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// An optional default value, if no value is specified.
        /// </summary>
        public IGraphValue? DefaultValue { get; set; } = null;

        /// <summary>
        /// Whether the argument has a default value.
        /// </summary>
        public bool HasDefaultValue => this.DefaultValue is not null;

        /// <summary>
        /// List of directives applied onto the argument.
        /// </summary>
        public readonly List<Directive> Directives = new();

        /// <summary>
        /// Whether the argument has any directives.
        /// </summary>
        public bool HasDirectives => this.Directives is { Count: > 0 };

        public ArgumentDefinition(
            string name,
            Type type,
            IGraphValue? defaultValue = null,
            List<Directive>? directives = null)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if(!type.IsAssignableTo(typeof(ITypeDefinition)))
            {
                throw new ArgumentException($"Argument '{name}' type must derive from {nameof(ITypeDefinition)}", nameof(type));
            }

            this.Name = name;
            this.Type = type;
            this.DefaultValue = defaultValue;

            if(directives is not null)
            {
                this.Directives.AddRange(directives);
            }
        }

        /// <inheritdoc />
        public GraphArgumentDefinition CreateSyntaxNode(IServiceProvider services)
        {
            // NOTE: Lifetimes are upheld when using Get*Service.
            GraphType type = services
                .GetRequiredService<ITypeResolver>()
                .ResolveDefinition(this.Type);

            return new()
            {
                Name = new GraphName(this.Name),
                Description = GraphDescription.From(this.Description),
                Type = type,
                DefaultValue = this.DefaultValue,
                Directives = new GraphDirectives
                {
                    Directives = this.Directives
                        .Select(v => v.CreateSyntaxNode(services))
                        .ToList()
                }
            };
        }
    }
}
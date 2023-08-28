using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    /// <summary>
    /// Definition of a scalar type.
    /// </summary>
    public abstract class ScalarType : TypeDefinition, ICreateDefinition<GraphScalarType>
    {
        /// <inheritdoc />
        public override string Name { get; protected set; }

        /// <summary>
        /// If the scalar is based on a specification (RFC or otherwise), it can be specified here.
        /// </summary>
        public string? SpecifiedBy { get; protected set; } = null;

        /// <summary>
        /// Resolver for converting leaf values to/from this scalar.
        /// </summary>
        public abstract ILeafValueResolver Resolver { get; protected set; }

        /// <summary>
        /// List of directives applied onto the type.
        /// </summary>
        public readonly List<Directive> Directives = new();

        /// <summary>
        /// Whether the type has any directives.
        /// </summary>
        public bool HasDirectives => this.Directives is { Count: > 0 };

        /// <summary>
        /// Whether the specified input value, <paramref name="value" />, is an instance of this scalar.
        /// </summary>
        public abstract bool IsOfType(IGraphValue value);

        /// <summary>
        /// Whether the specified result value, <paramref name="value" />, is an instance of this scalar.
        /// </summary>
        public abstract bool IsOfType(object? value);

        public ScalarType(string name, string? description = null, string? specifiedBy = null, Type? runtimeType = null)
        {
            this.Name = name;
            this.Description = description;
            this.SpecifiedBy = specifiedBy;
            this.RuntimeType = runtimeType;
        }

        public GraphScalarType CreateSyntaxNode(IServiceProvider services)
        {
            List<Directive> directives = new(this.Directives);

            if(!string.IsNullOrEmpty(this.SpecifiedBy))
            {
                Directive specifiedByDirective = new(SpecifiedByDirective.Alias);
                specifiedByDirective.Arguments.Add(new("url", new GraphStringValue(this.SpecifiedBy)));

                directives.Add(specifiedByDirective);
            }

            GraphScalarType definition = new()
            {
                Name = new GraphName(this.Name),
                Description = GraphDescription.From(this.Description),
                Directives = new GraphDirectives()
                {
                    Directives = directives
                        .Select(v => v.CreateSyntaxNode(services))
                        .ToList()
                }
            };

            return definition;
        }
    }
}
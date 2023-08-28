using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    /// <summary>
    /// Contract for a a single validation rule, matching a certain node type.
    /// </summary>
    /// <remarks>
    /// All validation rules are executed in parallel, so any state within the rule should remain unaltered.
    /// </remarks>
    public interface IValidationRule
    {
        /// <summary>
        /// Type of the context, which the validation rule uses as state.
        /// </summary>
        /// <remarks>
        /// Must be of type <see cref="ValidationContext" />.
        /// </remarks>
        Type ContextType { get; }

        /// <summary>
        /// Create a visitor for the current validation rule, which will walk the syntax-tree.
        /// </summary>
        /// <returns></returns>
        IValidationVisitor CreateVisitor();
    }

    /// <inheritdoc />
    public abstract class ValidationRule<TContext> : IValidationRule
        where TContext : ValidationContext
    {
        /// <inheritdoc />
        public Type ContextType => typeof(TContext);

        /// <inheritdoc />
        public abstract IValidationVisitor CreateVisitor();
    }
}
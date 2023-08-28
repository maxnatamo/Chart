using System.Collections.ObjectModel;

using Chart.Core.Execution;
using Chart.Language.SyntaxTree;
using Chart.Utilities;

using Microsoft.Extensions.Options;

namespace Chart.Core
{
    public interface IValidationExecutor
    {
        /// <summary>
        /// Validate the given document context. If any errors occur, they will be raised in the given context.
        /// </summary>
        /// <param name="context">The document context to validate.</param>
        /// <param name="stage">Which validation stage to execute.</param>
        /// <remarks>
        /// All validation rules are executed in parallel, so any state within the rule should remain unaltered.
        /// </remarks>
        Task ValidateAsync(
            DocumentValidationContext context,
            ValidationStage stage = ValidationStage.Enter,
            CancellationToken? cancellationToken = null);

        /// <summary>
        /// Validate the given schema context. If any errors occur, they will be raised in the given context.
        /// </summary>
        /// <param name="context">The schema context to validate.</param>
        /// <param name="stage">Which validation stage to execute.</param>
        /// <remarks>
        /// All validation rules are executed in parallel, so any state within the rule should remain unaltered.
        /// </remarks>
        Task ValidateAsync(
            SchemaValidationContext context,
            ValidationStage stage = ValidationStage.Enter,
            CancellationToken? cancellationToken = null);
    }

    public class ValidationExecutor : IValidationExecutor
    {
        private readonly IOptions<ValidationOptions> _options;
        private readonly DocumentTraverser _documentTraverser;
        private readonly IExecutionStrategySelector _executionStrategySelector;
        private readonly List<IValidationRule> _schemaVisitors = new();
        private readonly List<IValidationRule> _documentVisitors = new();

        public ValidationExecutor(
            IOptions<ValidationOptions> options,
            IEnumerable<IValidationRule> validationRules,
            IExecutionStrategySelector executionStrategySelector)
        {
            this._options = options;
            this._documentTraverser = new DocumentTraverser();
            this._executionStrategySelector = executionStrategySelector;

            foreach(IValidationRule rule in validationRules)
            {
                switch(rule.ContextType)
                {
                    case Type t when t == typeof(SchemaValidationContext):
                        this._schemaVisitors.Add(rule);
                        break;

                    case Type t when t == typeof(DocumentValidationContext):
                        this._documentVisitors.Add(rule);
                        break;

                    case Type t when t == typeof(ValidationContext):
                        this._documentVisitors.Add(rule);
                        this._schemaVisitors.Add(rule);
                        break;

                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <inheritdoc />
        public async Task ValidateAsync(
            DocumentValidationContext context,
            ValidationStage stage = ValidationStage.Enter,
            CancellationToken? cancellationToken = null) =>
            await this.ValidateContextAsync(
                context,
                this._documentVisitors,
                context.Query.GetAllDefinitions(),
                stage,
                cancellationToken ?? new CancellationToken(false));

        /// <inheritdoc />
        public async Task ValidateAsync(
            SchemaValidationContext context,
            ValidationStage stage = ValidationStage.Enter,
            CancellationToken? cancellationToken = null) =>
            await this.ValidateContextAsync(
                context,
                this._schemaVisitors,
                context.Schema.GetAllDefinitions(),
                stage,
                cancellationToken ?? new CancellationToken(false));

        private async Task ValidateContextAsync(
            ValidationContext context,
            List<IValidationRule> rules,
            ReadOnlyCollection<GraphDefinition> definitions,
            ValidationStage stage,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            context.ClearErrors();

            IExecutionStrategyImplementation executionStrategy =
                this._executionStrategySelector.Select(this._options.Value.ValidationStrategy);

            Type contextType = context.GetType();
            List<GraphNodeInfo> nodes = this._documentTraverser.Traverse(definitions);

            for(int i = 0; i < rules.Count; i++)
            {
                IValidationRule rule = rules[i];

                for(int j = 0; j < nodes.Count; j++)
                {
                    IGraphNode? node = nodes[j]?.Node;

                    if(node is null || !contextType.IsAssignableTo(rule.ContextType))
                    {
                        continue;
                    }

                    IValidationVisitor visitor = rule.CreateVisitor();
                    Func<Task> task = stage switch
                    {
                        ValidationStage.Enter => () => visitor.EnterAsync(node, context),
                        ValidationStage.Leave => () => visitor.LeaveAsync(node, context),

                        _ => throw new NotSupportedException()
                    };

                    executionStrategy.AddTask(task);
                }
            }

            await executionStrategy.Execute(cancellationToken);
        }
    }
}
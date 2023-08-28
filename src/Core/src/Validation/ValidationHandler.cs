namespace Chart.Core
{
    public interface IValidationHandler
    {
        /// <summary>
        /// Validate the current schema, at the given stage.
        /// </summary>
        Task<ValidationAction> ValidateAsync(
            ValidationStage stage,
            CancellationToken? cancellationToken = null);

        /// <summary>
        /// Validate the current execution context, at the given stage.
        /// </summary>
        Task<ValidationAction> ValidateAsync(
            QueryExecutionContext executionContext,
            ValidationStage stage,
            CancellationToken? cancellationToken = null);
    }

    /// <inheritdoc />
    public class DefaultValidationHandler : IValidationHandler
    {
        private readonly IValidationExecutor _validationExecutor;
        private readonly SchemaAccessor _schemaAccessor;
        private readonly ExecutionResultAccessor _executionResultAccessor;

        public DefaultValidationHandler(
            IValidationExecutor validationExecutor,
            SchemaAccessor schemaAccessor,
            ExecutionResultAccessor executionResultAccessor)
        {
            this._validationExecutor = validationExecutor;
            this._schemaAccessor = schemaAccessor;
            this._executionResultAccessor = executionResultAccessor;
        }

        /// <inheritdoc />
        public async Task<ValidationAction> ValidateAsync(
            ValidationStage stage,
            CancellationToken? cancellationToken = null)
        {
            SchemaValidationContext validationContext = new(this._schemaAccessor.Schema);
            await this._validationExecutor.ValidateAsync(validationContext, stage, cancellationToken);

            return this.HandleValidationResult(validationContext);
        }

        /// <inheritdoc />
        public async Task<ValidationAction> ValidateAsync(
            QueryExecutionContext executionContext,
            ValidationStage stage,
            CancellationToken? cancellationToken = null)
        {
            DocumentValidationContext validationContext = new(executionContext.Schema, executionContext.Query);
            await this._validationExecutor.ValidateAsync(validationContext, stage, cancellationToken);

            return this.HandleValidationResult(validationContext);
        }

        /// <summary>
        /// Handle the results of the validation executor.
        /// </summary>
        private ValidationAction HandleValidationResult(ValidationContext context)
        {
            ExecutionResult response = this._executionResultAccessor.Result;
            response.Errors.AddRange(context.Errors);

            return response.Errors.Any()
                ? ValidationAction.Abort
                : ValidationAction.Continue;
        }
    }
}
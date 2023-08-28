using Chart.Core.Instrumentation;
using Chart.Language.SyntaxTree;

namespace Chart.Core.Execution
{
    /// <summary>
    /// Middleware for resolving the correct operation from the query, if multiple operations are given.
    /// </summary>
    public class ValidationMiddleware : IRequestExecutionMiddleware
    {
        private readonly IValidationHandler _validationHandler;
        private readonly IExecutionEventRaiser _executionEventRaiser;

        public ValidationMiddleware(
            IValidationHandler validationHandler,
            IExecutionEventRaiser executionEventRaiser)
        {
            this._validationHandler = validationHandler;
            this._executionEventRaiser = executionEventRaiser;
        }

        /// <inheritdoc />
        public async ValueTask<ExecutionResult> InvokeAsync(
            QueryExecutionContext context,
            RequestExecutionDelegate next,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this._executionEventRaiser.ValidateDocument(context);

            // Errors are raised by the individual validation rules, so we just short-circuit here.

            if(await this._validationHandler.ValidateAsync(
                context,
                ValidationStage.Enter,
                cancellationToken) == ValidationAction.Abort)
            {
                return context.Result;
            }

            this._executionEventRaiser.ValidateDocumentFinished(context);

            ExecutionResult result = await next(context, cancellationToken);

            this._executionEventRaiser.ValidateDocument(context);

            if(await this._validationHandler.ValidateAsync(
                context,
                ValidationStage.Leave,
                cancellationToken) == ValidationAction.Abort)
            {
                // TODO: What would be the correct way of handling validation errors on the way back?
                return result;
            }

            this._executionEventRaiser.ValidateDocumentFinished(context);

            return result;
        }
    }
}
using Chart.Core.Instrumentation;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core
{
    public interface IQueryExecutor
    {
        Task<ExecutionResult> ExecuteAsync(QueryRequest request);
    }

    public class QueryExecutor : IQueryExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        internal QueryExecutor(Schema schema, IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._serviceProvider.UpdateSchema(schema);
        }

        internal QueryExecutor(Schema schema)
        {
            this._serviceProvider = schema.ServiceProvider;
            this._serviceProvider.UpdateSchema(schema);
        }

        public async Task<ExecutionResult> ExecuteAsync(QueryRequest request)
        {
            CancellationTokenSource cancellationTokenSource = new();

            IServerEventRaiser serverEventRaiser = this._serviceProvider
                .GetRequiredService<IServerEventRaiser>();

            this._serviceProvider.UpdateQuery(request);

            QueryExecutionContext executionContext = this._serviceProvider
                .GetRequiredService<IQueryExecutionContextAccessor>().Context;

            this._serviceProvider.UpdateResponse(executionContext.Result);

            IExecutionPipeline executionPipeline = this._serviceProvider.GetService<IExecutionPipeline>()
                ?? throw new NotSupportedException("No execution pipeline configured.");

            serverEventRaiser.RequestReceived(executionContext);

            ExecutionResult executionResult = await executionPipeline
                .ExecuteAsync(executionContext, cancellationTokenSource.Token);

            serverEventRaiser.RequestEnded(executionContext);

            return executionResult;
        }
    }
}
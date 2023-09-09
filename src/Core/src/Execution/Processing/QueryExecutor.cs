using Chart.Core.Instrumentation;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core
{
    public interface IQueryExecutor
    {
        Task<ExecutionResult> ExecuteAsync(
            string requestQuery,
            CancellationToken? cancellationToken = null);

        Task<ExecutionResult> ExecuteAsync(
            QueryRequest request,
            CancellationToken? cancellationToken = null);
    }

    public class QueryExecutor : IQueryExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        internal QueryExecutor(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;

            if(this._serviceProvider.GetService<SchemaAccessor>()?.Schema is null)
            {
                throw new NotImplementedException();
            }
        }

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

        public async Task<ExecutionResult> ExecuteAsync(
            string requestQuery,
            CancellationToken? cancellationToken = null)
        {
            QueryRequest request = new QueryRequestBuilder()
                .SetQuery(requestQuery)
                .Create();

            return await this.ExecuteAsync(request, cancellationToken);
        }

        public async Task<ExecutionResult> ExecuteAsync(
            QueryRequest request,
            CancellationToken? cancellationToken = null)
        {
            cancellationToken ??= new();

            IServerEventRaiser serverEventRaiser = this._serviceProvider
                .GetRequiredService<IServerEventRaiser>();

            this._serviceProvider.UpdateQuery(request);

            QueryExecutionContext executionContext = this._serviceProvider
                .GetRequiredService<IQueryExecutionContextAccessor>().Context;

            this._serviceProvider.UpdateResponse(executionContext.Result);

            serverEventRaiser.RequestReceived(executionContext);

            IExecutionPipeline executionPipeline = this._serviceProvider.GetService<IExecutionPipeline>()
                ?? throw new NotSupportedException("No execution pipeline configured.");

            ExecutionResult executionResult = await executionPipeline
                .ExecuteAsync(executionContext, cancellationToken.Value);

            serverEventRaiser.RequestEnded(executionContext);

            return executionResult;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core
{
    public static partial class RequestServiceExtensions
    {
        public static RequestServiceBuilder AddUtility(this RequestServiceBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.Services.AddScoped<INameFormatter, NameFormatter>();

            builder.Services.AddSingleton<SchemaAccessor>();
            builder.Services.AddScoped<QueryRequestAccessor>();
            builder.Services.AddScoped<ExecutionResultAccessor>();
            builder.Services.AddScoped<IQueryExecutionContextAccessor, QueryExecutionContextAccessor>();

            return builder;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core
{
    public static partial class IServiceProviderExtensions
    {
        /// <summary>
        /// Internal utility for updating the schema in the current scope.
        /// </summary>
        /// <remarks>
        /// If the schema is updated, the previous schema will exist until the
        /// last request using it is finished.
        /// </remarks>
        internal static IServiceProvider UpdateSchema(this IServiceProvider provider, Schema schema)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            provider.GetRequiredService<SchemaAccessor>().Schema = schema;
            return provider;
        }

        /// <summary>
        /// Internal utility for updating the query in the current scope.
        /// </summary>
        internal static IServiceProvider UpdateQuery(this IServiceProvider provider, QueryRequest query)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            provider.GetRequiredService<QueryRequestAccessor>().Request = query;
            return provider;
        }

        /// <summary>
        /// Internal utility for updating the response in the current scope.
        /// </summary>
        internal static IServiceProvider UpdateResponse(this IServiceProvider provider, ExecutionResult response)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            provider.GetRequiredService<ExecutionResultAccessor>().Result = response;
            return provider;
        }
    }
}
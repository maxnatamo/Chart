using Chart.Core.Execution;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core
{
    public static partial class IServiceCollectionExtensions
    {
        public static RequestServiceBuilder AddChart(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            RequestServiceBuilder builder = new(services);

            // Core services
            builder.AddTypeServices();
            builder.AddPipelineExecution();
            builder.AddValidation();
            builder.AddUtility();

            return builder;
        }
    }
}
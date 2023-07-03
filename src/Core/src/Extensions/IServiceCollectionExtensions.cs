using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddChart(this IServiceCollection services)
        {
            if(services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();

            // Core services
            services.AddTypeServices();

            return services;
        }
    }
}
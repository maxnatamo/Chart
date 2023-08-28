using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core
{
    public static class ServiceProviderHelper
    {
        public static RequestServiceBuilder DefaultCollection() =>
            new ServiceCollection()
                .AddChart();

        public static IServiceProvider DefaultProvider() =>
            ServiceProviderHelper
                .DefaultCollection()
                .BuildServiceProvider();
    }
}
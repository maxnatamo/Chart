using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTypeServices(this IServiceCollection services)
        {
            if(services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Configure<TypeRegistry>(_ => {});
            services.AddScoped<INameFormatter, NameFormatter>();

            services.AddScoped<DescriptorContext>();
            services.AddScoped<IObjectFieldDescriptorFactory, ObjectFieldDescriptorFactory>();

            return services;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Scion.Infrastructure.DynamicProperties;

namespace Scion.Data.CommonDynamicProperties
{
    public static class ServiceCollectionExtenions
    {
        public static IServiceCollection AddDynamicProperties(this IServiceCollection services)
        {
            services.AddSingleton<IDynamicPropertyService, DynamicPropertyService>();
            services.AddSingleton<IDynamicPropertySearchService, DynamicPropertySearchService>();
            services.AddSingleton<IDynamicPropertyRegistrar, DynamicPropertyService>();
            services.AddSingleton<IDynamicPropertyDictionaryItemsSearchService, DynamicPropertyDictionaryItemsSearchService>();
            services.AddSingleton<IDynamicPropertyDictionaryItemsService, DynamicPropertyDictionaryItemsService>();

            return services;
        }
    }
}

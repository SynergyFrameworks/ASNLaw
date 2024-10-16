using Microsoft.Extensions.DependencyInjection;
using Scion.Infrastructure.Settings;

namespace Scion.Data.Common.Settings
{
    public static class ServiceCollectionExtenions
    {
        public static IServiceCollection AddSettings(this IServiceCollection services)
        {
            services.AddSingleton<ISettingsManager, SettingsManager>();
            services.AddSingleton<ISettingsRegistrar>(context => context.GetService<ISettingsManager>());
            services.AddSingleton<ISettingsSearchService, SettingsSearchService>();

            return services;
        }
    }
}

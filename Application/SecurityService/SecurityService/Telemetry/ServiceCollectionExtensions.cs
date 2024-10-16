using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scion.Infrastructure.Telemetry;
using Scion.Infrastructure.Web.Telemetry;

namespace Scion.Infrastructure.Web.Telemetry
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Charge AppInsights options and add the telemetry
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppInsightsTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            // The following lines enables Application Insights telemetry collection.
            // As we use ApplicationInsights.AspNetCore SDK 2.15.0 & above, please read there about the standard way of service options configuration:
            // https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core#configuration-recommendation-for-microsoftapplicationinsightsaspnetcore-sdk-2150--above
            // See also the configurable settings in ApplicationInsightsServiceOptions for the most up-to-date list:
            // https://github.com/microsoft/ApplicationInsights-dotnet/blob/develop/NETCORE/src/Shared/Extensions/ApplicationInsightsServiceOptions.cs
            services.AddApplicationInsightsTelemetry();

            // Register app insights extensions
            services.AddSingleton<ITelemetryInitializer, UserTelemetryInitializer>();

            // Disable adaptive sampling before custom configuration to have a choice between processors in Configure,
            // according to instructions: https://docs.microsoft.com/en-us/azure/azure-monitor/app/sampling#configure-sampling-settings
            services.Configure((ApplicationInsightsServiceOptions o) => o.EnableAdaptiveSampling = false);

            // Always ignore SignalRTelemetry
            services.AddApplicationInsightsTelemetryProcessor<IgnoreSignalRTelemetryProcessor>();

            // Charge ApplicationInsights options to enable custom configuration of sampling processors
            var aiScionOptionsSection = configuration.GetSection("ScionAnalytics:ApplicationInsights");
            services.AddOptions<ApplicationInsightsOptions>().Bind(aiScionOptionsSection);

            var aiScionOptions = aiScionOptionsSection.Get<ApplicationInsightsOptions>() ?? new ApplicationInsightsOptions();

            // Enable SQL dependencies filtering, if need            
            if (aiScionOptions.IgnoreSqlTelemetryOptions != null)
            {
                services.AddApplicationInsightsTelemetryProcessor<IgnoreSqlTelemetryProcessor>();
            }

            // Force SQL reflection in dependencies, if need
            // See instructions here: https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-dependencies#advanced-sql-tracking-to-get-full-sql-query
            services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) => { module.EnableSqlCommandTextInstrumentation = aiScionOptions.EnableSqlCommandTextInstrumentation || aiScionOptions.EnableLocalSqlCommandTextInstrumentation; });

            return services;
        }
    }
}

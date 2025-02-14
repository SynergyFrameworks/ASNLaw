using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Common.Bus;
using Infrastructure.Settings.Events;
using Infrastructure.Settings;

namespace Hangfire
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHangfire(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseHangfireDashboard("/hangfire", new DashboardOptions { Authorization = new[] { new HangfireAuthorizationHandler() } });

            var mvcJsonOptions = appBuilder.ApplicationServices.GetService<IOptions<MvcNewtonsoftJsonOptions>>();
            GlobalConfiguration.Configuration.UseSerializerSettings(mvcJsonOptions.Value.SerializerSettings);

            var inProcessBus = appBuilder.ApplicationServices.GetService<IHandlerRegistrar>();
            var recurringJobManager = appBuilder.ApplicationServices.GetService<IRecurringJobManager>();
            var settingsManager = appBuilder.ApplicationServices.GetService<ISettingsManager>();
           inProcessBus.RegisterHandler<ObjectSettingChangedEvent>(async (message, token) => await recurringJobManager.HandleSettingChangeAsync(settingsManager, message));

            return appBuilder;
        }
    }
}

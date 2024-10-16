using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scion.Data.Common.Extensions;
using Scion.Data.Common.Repositories;
using Scion.Hangfire;
using Scion.Hangfire.Middleware;
using Scion.Infrastructure.Bus;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Security;
using Scion.Infrastructure.Security.Repositories;
using Scion.Infrastructure.Settings;
using Scion.Infrastructure.Settings.Events;

namespace Scion.Infrastructure.Web.Migrations
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Apply platform migrations
        /// </summary>
        /// <param name="appBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UsePlatformMigrations(this IApplicationBuilder appBuilder)
        {
            using (IServiceScope serviceScope = appBuilder.ApplicationServices.CreateScope())
            {
                PlatformDbContext platformDbContext = serviceScope.ServiceProvider.GetRequiredService<PlatformDbContext>();
                platformDbContext.Database.MigrateIfNotApplied(MigrationName.GetUpdateV2MigrationName("Platform"));
                platformDbContext.Database.Migrate();

                SecurityDbContext securityDbContext = serviceScope.ServiceProvider.GetRequiredService<SecurityDbContext>();
                securityDbContext.Database.MigrateIfNotApplied(MigrationName.GetUpdateV2MigrationName("Security"));
                securityDbContext.Database.Migrate();
            }

            return appBuilder;
        }


        public static IApplicationBuilder UseHangfire(this IApplicationBuilder appBuilder, IConfiguration configuration)
        {
            // Always resolve Hangfire.IGlobalConfiguration to enforce correct initialization of Hangfire giblets before modules init.
            // Don't remove next line, it will cause issues with modules startup at hangfire-less (UseHangfireServer=false) platform instances.
            IGlobalConfiguration hangfireGlobalConfiguration = appBuilder.ApplicationServices.GetRequiredService<IGlobalConfiguration>();

            // This is an important workaround of Hangfire initialization issues
            // The standard database schema initialization way described at the page https://docs.hangfire.io/en/latest/configuration/using-sql-server.html works on an existing database only.
            // Therefore we create SqlServerStorage for Hangfire manually here.
            // This way we ensure Hangfire schema will be applied to storage AFTER platform database creation.
            HangfireOptions hangfireOptions = appBuilder.ApplicationServices.GetRequiredService<IOptions<HangfireOptions>>().Value;
            if (hangfireOptions.JobStorageType == HangfireJobStorageType.SqlServer)
            {
                SqlServerStorage storage = new SqlServerStorage(configuration.GetConnectionString("ScionAnalytics"), hangfireOptions.SqlServerStorageOptions);
                hangfireGlobalConfiguration.UseStorage(storage);
            }

            appBuilder.UseHangfireDashboard("/hangfire", new DashboardOptions { Authorization = new[] { new HangfireAuthorizationHandler() } });

            IOptions<MvcNewtonsoftJsonOptions> mvcJsonOptions = appBuilder.ApplicationServices.GetService<IOptions<MvcNewtonsoftJsonOptions>>();
            GlobalConfiguration.Configuration.UseSerializerSettings(mvcJsonOptions.Value.SerializerSettings);

            IHandlerRegistrar inProcessBus = appBuilder.ApplicationServices.GetService<IHandlerRegistrar>();
            IRecurringJobManager recurringJobManager = appBuilder.ApplicationServices.GetService<IRecurringJobManager>();
            ISettingsManager settingsManager = appBuilder.ApplicationServices.GetService<ISettingsManager>();
            inProcessBus.RegisterHandler<ObjectSettingChangedEvent>(async (message, token) => await recurringJobManager.HandleSettingChangeAsync(settingsManager, message));

            // Add Hangfire filters/middlewares
            IUserNameResolver userNameResolver = appBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IUserNameResolver>();
            GlobalJobFilters.Filters.Add(new HangfireUserContextMiddleware(userNameResolver));

            return appBuilder;
        }






    }
}

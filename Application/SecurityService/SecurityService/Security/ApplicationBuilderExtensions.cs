using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Scion.Infrastructure.Bus;
using Scion.Infrastructure.Common;
using Scion.Hangfire;
using Scion.Infrastructure.Security;
using Scion.Infrastructure.Security.Events;
using Scion.Infrastructure.Security.Handlers;
using Scion.Infrastructure.Settings;
using Scion.Infrastructure.Web.Security.BackgroundJobs;
using System.Linq;
using System.Threading.Tasks;

namespace Scion.Infrastructure.Web.Security
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder UsePlatformPermissions(this IApplicationBuilder appBuilder)
        {
            //Register PermissionScope type itself to prevent Json serialization error due that fact that will be taken from other derived from PermissionScope type (first in registered types list) in PolymorphJsonContractResolver
            AbstractTypeFactory<PermissionScope>.RegisterType<PermissionScope>();

            IPermissionsRegistrar permissionsProvider = appBuilder.ApplicationServices.GetRequiredService<IPermissionsRegistrar>();
            permissionsProvider.RegisterPermissions(PlatformConstants.Security.Permissions.AllPermissions.Select(x => new Permission() { GroupName = "Platform", Name = x }).ToArray());
            return appBuilder;
        }

        public static IApplicationBuilder UseSecurityHandlers(this IApplicationBuilder appBuilder)
        {
            IHandlerRegistrar inProcessBus = appBuilder.ApplicationServices.GetService<IHandlerRegistrar>();
            inProcessBus.RegisterHandler<UserChangedEvent>(async (message, token) => await appBuilder.ApplicationServices.GetService<LogChangesUserChangedEventHandler>().Handle(message));
            inProcessBus.RegisterHandler<UserChangedEvent>(async (message, token) => await appBuilder.ApplicationServices.GetService<UserApiKeyActualizeEventHandler>().Handle(message));
            inProcessBus.RegisterHandler<UserPasswordChangedEvent>(async (message, token) => await appBuilder.ApplicationServices.GetService<LogChangesUserChangedEventHandler>().Handle(message));
            inProcessBus.RegisterHandler<UserResetPasswordEvent>(async (message, token) => await appBuilder.ApplicationServices.GetService<LogChangesUserChangedEventHandler>().Handle(message));
            inProcessBus.RegisterHandler<UserLoginEvent>(async (message, token) => await appBuilder.ApplicationServices.GetService<LogChangesUserChangedEventHandler>().Handle(message));
            inProcessBus.RegisterHandler<UserLogoutEvent>(async (message, token) => await appBuilder.ApplicationServices.GetService<LogChangesUserChangedEventHandler>().Handle(message));
            inProcessBus.RegisterHandler<UserRoleAddedEvent>(async (message, token) => await appBuilder.ApplicationServices.GetService<LogChangesUserChangedEventHandler>().Handle(message));
            inProcessBus.RegisterHandler<UserRoleRemovedEvent>(async (message, token) => await appBuilder.ApplicationServices.GetService<LogChangesUserChangedEventHandler>().Handle(message));

            return appBuilder;
        }

        /// <summary>
        /// Schedule a periodic job for prune expired/invalid authorization tokens
        /// </summary>
        /// <param name="appBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UsePruneExpiredTokensJob(this IApplicationBuilder appBuilder)
        {
            IRecurringJobManager recurringJobManager = appBuilder.ApplicationServices.GetService<IRecurringJobManager>();
            ISettingsManager settingsManager = appBuilder.ApplicationServices.GetService<ISettingsManager>();

            recurringJobManager.WatchJobSetting(
                settingsManager,
                new SettingCronJobBuilder()
                    .SetEnablerSetting(PlatformConstants.Settings.Security.EnablePruneExpiredTokensJob)
                    .SetCronSetting(PlatformConstants.Settings.Security.CronPruneExpiredTokensJob)
                    .ToJob<PruneExpiredTokensJob>(x => x.Process())
                    .Build());

            return appBuilder;
        }

        public static async Task<IApplicationBuilder> UseDefaultUsersAsync(this IApplicationBuilder appBuilder)
        {
            using (IServiceScope scope = appBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();


                if (await userManager.FindByNameAsync("admin") == null)
                {
#pragma warning disable S2068 // disable check: 'password' detected in this expression, review this potentially hardcoded credential
                    ApplicationUser admin = new ApplicationUser
                    {
                        Id = "1eb2fa8ac6574541afdb525833dadb46",
                        IsAdministrator = true,
                        UserName = "admin",
                        PasswordHash = "AHQSmKnSLYrzj9vtdDWWnUXojjpmuDW2cHvWloGL9UL3TC9UCfBmbIuR2YCyg4BpNg==",
                        PasswordExpired = true,
                        Email = "admin@democap.com"
                    };
#pragma warning restore S2068 // disable check: 'password' detected in this expression, review this potentially hardcoded credential
                    ApplicationUser adminUser = await userManager.FindByIdAsync(admin.Id);
                    if (adminUser == null)
                    {
                        await userManager.CreateAsync(admin);
                    }
                }

            }
            return appBuilder;
        }
    }

}

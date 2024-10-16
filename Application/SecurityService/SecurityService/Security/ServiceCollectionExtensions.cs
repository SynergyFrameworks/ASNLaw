using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scion.Business.Security;
using Scion.Business.Security.Services;
using Scion.Infrastructure.ChangeLog;
using Scion.Infrastructure.Security;
using Scion.Infrastructure.Security.Handlers;
using Scion.Infrastructure.Security.Repositories;
using Scion.Infrastructure.Security.Search;
using System;


namespace Scion.Infrastructure.Web.Security
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSecurityServices(this IServiceCollection services, Action<AuthorizationOptions> setupAction = null)
        {
            services.AddTransient<ISecurityRepository, SecurityRepository>();
            services.AddTransient<Func<ISecurityRepository>>(provider => () => provider.CreateScope().ServiceProvider.GetService<ISecurityRepository>());

            services.AddScoped<IUserApiKeyService, Scion.Business.Security.Services.UserApiKeyService>();
            services.AddScoped<IUserApiKeySearchService, Scion.Business.Security.Services.UserApiKeySearchService>();

            services.AddScoped<IUserNameResolver, HttpContextUserResolver>();
            services.AddSingleton<IPermissionsRegistrar, Scion.Business.Security.DefaultPermissionProvider>();
            services.AddScoped<IRoleSearchService, Scion.Business.Security.Services.RoleSearchService>();

            //Register as singleton because this abstraction can be used as dependency in singleton services
            services.AddSingleton<IUserSearchService>(provider =>
            {
                IServiceScope scope = provider.CreateScope();
                return new Scion.Business.Security.Services.UserSearchService(scope.ServiceProvider.GetService<Func<UserManager<ApplicationUser>>>(),
                                             scope.ServiceProvider.GetService<Func<RoleManager<Role>>>());
            });

            //Identity dependencies override
            services.TryAddScoped<RoleManager<Role>, CustomRoleManager>();
            services.TryAddScoped<UserManager<ApplicationUser>, CustomUserManager>();
            services.TryAddScoped<IPasswordValidator<ApplicationUser>, CustomPasswordValidator>();
            services.TryAddScoped<IdentityErrorDescriber, CustomIdentityErrorDescriber>();
            services.AddSingleton<Func<RoleManager<Role>>>(provider => () => provider.CreateScope().ServiceProvider.GetService<RoleManager<Role>>());
            services.AddSingleton<Func<UserManager<ApplicationUser>>>(provider => () => provider.CreateScope().ServiceProvider.GetService<UserManager<ApplicationUser>>());
            services.AddSingleton<Func<SignInManager<ApplicationUser>>>(provider => () => provider.CreateScope().ServiceProvider.GetService<SignInManager<ApplicationUser>>());
            services.AddSingleton<IUserPasswordHasher, DefaultUserPasswordHasher>();
            //Use custom ClaimsPrincipalFactory to add system roles claims for user principal
            services.TryAddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, Scion.Business.Security.CustomUserClaimsPrincipalFactory>();

            if (setupAction != null)
            {
                services.Configure(setupAction);
            }

            services.AddSingleton(provider => new LogChangesUserChangedEventHandler(provider.CreateScope().ServiceProvider.GetService<IChangeLogService>()));
            services.AddSingleton(provider => new UserApiKeyActualizeEventHandler(provider.CreateScope().ServiceProvider.GetService<IUserApiKeyService>()));

            return services;
        }
    }
}

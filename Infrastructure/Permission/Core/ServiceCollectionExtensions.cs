// Copyright (c) Brock Allen, Dominick Baier, Michele Leroux Bustamante. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Infrastructure.Permissions.Runtime.Client;
using Infrastructure.Permissions.Contracts;
using System;

namespace Microsoft.Extensions.DependencyInjection
{

    /// <summary>
    /// Helper class to configure DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the policy server client.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static PermissionsBuilder AddPermissions<TPermissionProvider>(this IServiceCollection services) where TPermissionProvider : class, IPermissionProvider
        {
            //IConfiguration configuration
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddSingleton<IPermissionProvider, TPermissionProvider>();
            return new PermissionsBuilder(services);
        }
    }

}
// Copyright (c) Brock Allen, Dominick Baier, Michele Leroux Bustamante. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Infrastructure.Permissions.Contracts;
using Infrastructure.Permissions.Models;
using Infrastructure.Permissions.Runtime.Client;
using Serilog;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Permissions.Runtime.Client
{
    /// <summary>
    /// PolicyServer client
    /// </summary>
    public class PermissionService : IPermissionService
    {

        private readonly Policy _policy;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly ILogger _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionService"/> class.
        /// </summary>
        /// <param name="policy">The policy.</param>
        public PermissionService(IPermissionProvider permissionProvider, IConnectionMultiplexer connectionMultiplexer, ILogger log)
        {

            //_log = log;
            _connectionMultiplexer = connectionMultiplexer;
            _policy = new Policy(permissionProvider, _connectionMultiplexer, _log);
        }

        /// <summary>
        /// Determines whether the user is in a role.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        public async Task<bool> IsInRoleAsync(ClaimsPrincipal user, string role)
        {
            var policy = await EvaluateAsync(user);
            return policy.Roles.Contains(role);
        }

        /// <summary>
        /// Determines whether the user has a permission.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        public async Task<bool> HasPermissionAsync(ClaimsPrincipal user, string permission)
        {
            var policy = await EvaluateAsync(user);
            return policy.Permissions.Contains(permission);
        }

        /// <summary>
        /// Evaluates the policy for a given user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">user</exception>
        public Task<PermissionsResult> EvaluateAsync(ClaimsPrincipal user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return _policy.EvaluateAsync(user);
        }
    }
}
// Copyright (c) Brock Allen, Dominick Baier, Michele Leroux Bustamante. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Infrastructure.Permissions.Runtime.Client.AspNetCore
{
    internal class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService _client;

        public PermissionHandler(IPermissionService client)
        {
            _client = client;
        }

        public override Task HandleAsync(AuthorizationHandlerContext context)
        {
            return base.HandleAsync(context);
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (await _client.HasPermissionAsync(context.User, requirement.Name))
            {
                context.Succeed(requirement);
            }
        }
    }
}
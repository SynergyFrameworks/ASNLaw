using System;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Security;

namespace Scion.Data.Common.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDbTriggers(this IApplicationBuilder appBuilder)
        {
            Triggers<IAuditable>.Inserting += entry =>
            {
                var currentUserNameResolver = appBuilder.ApplicationServices.CreateScope().ServiceProvider.GetService<IUserNameResolver>();
                var currentTime = DateTime.UtcNow;
                var userName = currentUserNameResolver.GetCurrentUserName();

                entry.Entity.CreatedDate = entry.Entity.CreatedDate == default ? currentTime : entry.Entity.CreatedDate;
                entry.Entity.ModifiedDate = entry.Entity.CreatedDate;
                entry.Entity.CreatedBy = entry.Entity.CreatedBy ?? userName;
                entry.Entity.ModifiedBy = entry.Entity.CreatedBy;
            };

            Triggers<IAuditable>.Updating += entry =>
            {
                var currentUserNameResolver = appBuilder.ApplicationServices.CreateScope().ServiceProvider.GetService<IUserNameResolver>();
                var currentTime = DateTime.UtcNow;
                var userName = currentUserNameResolver.GetCurrentUserName();

                entry.Entity.ModifiedDate = currentTime;
                entry.Entity.ModifiedBy = userName;
            };
            return appBuilder;
        }
    }
}

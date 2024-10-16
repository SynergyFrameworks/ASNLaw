using Datalayer.Domain.Security;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datalayer.Seed
{
    public static class SeedCapUserPermissions
    {
        public static void EnsureSuperPermnissionsSeeded(this Context.ASNDbContext context)
        {
            var SuperUserId = Guid.NewGuid();    //Guid.Parse("4ad967ba-ef62-44c6-9eca-3134de1c0e01");
            var SuperUser = context.Users.FirstOrDefault(x => x.IdentityUserId == SuperUserId);

            if (SuperUser.Permissions.Count == 0)
            {
                var superAdminPermissions = context.Permissions.Where(x => x.AspNetRole == "SuperAdmin");
                SuperUser.Permissions = new List<Permission>();

                foreach (var permission in superAdminPermissions)
                {
                    SuperUser.Permissions.Add(permission);
                }
                context.SaveChanges();
            }
            else
            {
                Log.Information("Permissions exist");
            }
        }
    }
}

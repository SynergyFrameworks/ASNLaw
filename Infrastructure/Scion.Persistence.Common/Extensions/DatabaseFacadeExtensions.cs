using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Extensions;

namespace Scion.Data.Common.Extensions
{
    public static class DatabaseFacadeExtensions
    {
        public static void MigrateIfNotApplied(this DatabaseFacade databaseFacade, string targetMigration)
        {
            var connectionTimeout = databaseFacade.GetDbConnection().ConnectionTimeout;
            databaseFacade.SetCommandTimeout(connectionTimeout);

            var platformMigrator = databaseFacade.GetService<IMigrator>();
            var appliedMigrations = databaseFacade.GetAppliedMigrations();
            if (!appliedMigrations.Any(x => x.EqualsInvariant(targetMigration)))
            {
                platformMigrator.Migrate(targetMigration);
            }
        }
    }
}

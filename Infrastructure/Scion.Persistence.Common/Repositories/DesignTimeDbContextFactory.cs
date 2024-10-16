using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Scion.Data.Common.Repositories
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PlatformDbContext>
    {
        public PlatformDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PlatformDbContext>();

            builder.UseSqlServer("Data Source=(local);Initial Catalog=ScionAnalytics;Persist Security Info=True;User ID=Scion;Password=password;MultipleActiveResultSets=True;Connect Timeout=30");

            return new PlatformDbContext(builder.Options);
        }
    }
}

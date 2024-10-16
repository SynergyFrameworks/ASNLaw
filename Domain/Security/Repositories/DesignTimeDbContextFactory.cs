using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Scion.Business.Security.Repositories
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SecurityDbContext>
    {
        public SecurityDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SecurityDbContext>();

            builder.UseSqlServer("Data Source=(local);Initial Catalog=ScionAnalyticsGlobal;Persist Security Info=True;User ID=scion;Password=scion;MultipleActiveResultSets=True;Connect Timeout=30");
            builder.UseOpenIddict();
            return new SecurityDbContext(builder.Options);
        }
    }
}

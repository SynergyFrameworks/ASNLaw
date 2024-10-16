
using Microsoft.EntityFrameworkCore;
using Datalayer.Domain;

namespace Datalayer.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }

        public DbSet<Organization> Organization { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //.Build(builder);
        }
    }
}

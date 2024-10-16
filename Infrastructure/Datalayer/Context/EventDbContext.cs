using Microsoft.EntityFrameworkCore;
using Datalayer.Domain;

namespace Datalayer.Context
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
        { }

        public DbSet<Organization> Organization { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //.Build(builder);
        }
    }
}

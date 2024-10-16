using Microsoft.EntityFrameworkCore;
using Datalayer.Contracts;

namespace Datalayer.Extensions
{
    public static class BuilderExtention
    {
        public static void SetupColumnId<T>(this ModelBuilder modelBuilder) where T : class, IEntity
        {
            modelBuilder.Entity<T>()
                .Property(o => o.Id).HasDefaultValueSql("NEWID()");
        }
    }

}


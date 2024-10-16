using System;
using Microsoft.EntityFrameworkCore;

namespace Scion.Data.Common.Repositories.ParseParameter
{
    public class ParseParametersDbContext : DbContext
    {
        private string sqlServer = "81TPAWNLT14207"; //change to SA server, not using sqlserver on docker
        private string databaseName = "ScionAnalyticsGlobal";

        public ParseParametersDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@$"Data Source={sqlServer};Initial Catalog={databaseName};Integrated Security=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Model.ParseParameter>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Model.ParseParameter>().HasData(
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "#####", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "#.", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "##.", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "##.#", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "##.##", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "#.#*", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*-#", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*-##", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*-###", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*-####", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*-#.*", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*##)", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*##.#", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*#)", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*#.#", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*.#", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*.##", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*.##(?)", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*.##.", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*.##.*]", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*.##.*].", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*.##]", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*.#(?)", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*.#.", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*.#]", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*.#].", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*.[a-z].", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*.[A-Z].", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*[a-z])", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*[A-Z])", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*[a-z].", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "*[A-Z].", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "[a-z]##.", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "[A-Z]##.", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "[a-z]#.", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "[A-Z]#.", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "[a-z].#", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "[A-Z].#", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "[a-z].#*", CreatedBy = "System", CreatedDate = DateTime.UtcNow },
                  new Model.ParseParameter() { Id = Guid.NewGuid(), Parameter = "[A-Z].#*", CreatedBy = "System", CreatedDate = DateTime.UtcNow }
                  );
        }
        public DbSet<Model.ParseParameter> Parameters { get; set; }


    }
}

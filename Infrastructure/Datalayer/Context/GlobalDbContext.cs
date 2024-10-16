using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Datalayer.Domain;
using Datalayer.Enum;
using System;
using Domain.Parse.Model;

namespace Datalayer.Context
{
    public class GlobalDbContext : DbContext
    {
        private string _connectionString;

        public GlobalDbContext()
        {
        }

        public GlobalDbContext(DbContextOptions options) : base(options)
        {
        }

        //DBSETS
        public DbSet<ParseResult> ParseResults { get; set; }
        public DbSet<Parameter> Parameters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ALWAYS GET NEW CONNECTION STRING
            if (string.IsNullOrEmpty(_connectionString))
                LoadConnectionString();

            optionsBuilder.UseSqlServer(_connectionString);
        }

        //SEED INITIAL DATA
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Parameter>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Parameter>().HasData(
                  //PARSE
                  new Parameter() { Id = Guid.NewGuid(), Value = "#####", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 1, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "#.", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 2, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "##.", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 3, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "##.#", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 4, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "##.##", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 5, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "#.#*", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 6, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*-#", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 7, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*-##", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 8, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*-###", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 9, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*-####", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 10, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*-#.*", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 11, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*##)", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 12, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*##.#", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 13, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*#)", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 14, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*#.#", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 15, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*.#", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 16, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*.##", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 17, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*.##(?)", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 18, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*.##.", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 19, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*.##.*]", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 20, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*.##.*].", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 21, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*.##]", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 22, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*.#(?)", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 23, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*.#.", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 24, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*.#]", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 25, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*.#].", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 26, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*.[a-z].", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 27, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*.[A-Z].", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 28, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*[a-z])", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 29, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*[A-Z])", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 30, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*[a-z].", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 31, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "*[A-Z].", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 32, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "[a-z]##.", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 33, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "[A-Z]##.", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 34, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "[a-z]#.", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 35, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "[A-Z]#.", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 36, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "[a-z].#", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 37, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "[A-Z].#", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 38, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "[a-z].#*", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 39, Type = ParameterType.Parse },
                  new Parameter() { Id = Guid.NewGuid(), Value = "[A-Z].#*", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 40, Type = ParameterType.Parse },
                  //RESTRICTED
                  new Parameter() { Id = Guid.NewGuid(), Value = "I", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "II", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "III", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "IV", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "V", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "VI", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "VII", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "VIII", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "IX", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "X", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "J", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "1", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "2", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "3", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "4", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "5", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "6", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "7", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "8", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "9", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "10", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "ONE", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "TWO", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "THREE", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "FOUR", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "FIVE", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "SIX", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "SEVEN", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "EIGHT", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "NINE", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted },
                  new Parameter() { Id = Guid.NewGuid(), Value = "TEN", CreatedBy = "System", CreatedDate = DateTime.UtcNow, Weight = 0, Type = ParameterType.Restricted }
                  );

        }

        private void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            //DO NOT CHANGE
            _connectionString = configuration.GetConnectionString("GlobalDbConnection");
        }


    }
}

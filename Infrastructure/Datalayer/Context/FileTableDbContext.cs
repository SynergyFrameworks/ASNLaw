using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Datalayer.Domain;

namespace Datalayer.Context
{
    public class FileTableDbContext : DbContext
    {
        private string _connectionString;

        public FileTableDbContext()
        {

        }
        public FileTableDbContext(DbContextOptions options) : base(options)
        { 
        }

        public DbSet<FileDescription> FileDescriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ALWAYS GET NEW CONNECTION STRING
            if (string.IsNullOrEmpty(_connectionString))
                LoadConnectionString();

            optionsBuilder.UseSqlServer(_connectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FileDescription>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }
        private void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            //DO NOT CHANGE
            _connectionString = configuration.GetConnectionString("FileTableDbConnection");
        }
    }
}


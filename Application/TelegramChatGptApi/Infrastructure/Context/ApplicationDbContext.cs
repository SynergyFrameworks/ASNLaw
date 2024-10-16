using Microsoft.EntityFrameworkCore;
using CData.EntityFrameworkCore.Neo4j;
using TelegramChatGptApi.Application.DTOs;

namespace TelegramChatGptApi.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ChatChannel> ChatChannels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseNeo4j("bolt://neo4j:your_password@localhost:7687");
                }
            }
        }
    }
}

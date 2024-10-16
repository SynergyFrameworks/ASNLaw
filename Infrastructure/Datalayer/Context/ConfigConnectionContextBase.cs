using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Datalayer.Context
{
    public abstract class ConfigConnectionContextBase : DbContext
    {
        private string _connectionString;
        private readonly string _connectionConfigKey;
        private const string _APP_SETTINGS_KEY = "appsettings.json";

        public bool CanSeed { get; set; }

        public ConfigConnectionContextBase(string connectionConfigKey) : base()
        {
            _connectionConfigKey = connectionConfigKey;
        }

        /// <summary>
        /// Initializes the DbContext with a given connection key.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="connectionConfigKey"></param>
        public ConfigConnectionContextBase(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (string.IsNullOrEmpty(_connectionString))
                    LoadConnectionString();

                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        private void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(_APP_SETTINGS_KEY, optional: false);

            var configuration = builder.Build();
            _connectionString = configuration.GetConnectionString(_connectionConfigKey);

            configuration.Bind("CanRunSeed", this.CanSeed);
        }
    }
}

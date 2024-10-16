using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Datalayer.Context;

namespace Datalayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlConnection = configuration.GetConnectionString("LawDbConnection");

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(sqlConnection));

            // Register other services here...

            return services;
        }
    }
}

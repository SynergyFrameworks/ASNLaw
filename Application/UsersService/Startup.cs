using System;
using System.Reflection;
using System.IO;

using Scion.Persistence.Users;
using Scion.Events;
using Scion.Infrastructure.Consul;
using Scion.Infrastructure;
using Scion.Infrastructure.Logging;
using Scion.Infrastructure.MessageBrokers;
using Scion.Infrastructure.Outbox;
using Scion.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace UsersService
{
    public class Startup
    {
        private readonly IConfigurationRoot Configuration;
        private readonly IWebHostEnvironment environment;

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", reloadOnChange: true, optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            Log.Logger = LoggingExtensions.AddLogging(Configuration);

            environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString(ConnectionStringKeys.App)));

            services
                .AddConsul(Configuration)
                .AddMessageBroker(Configuration)
                .AddOutbox(Configuration)
                .AddSwagger(Configuration)
                .AddCore(typeof(Startup), typeof(EventsExtensions), typeof(DatabaseContext)); // Types are needed for mediator to work the different projects. In this case startup is added for this project and DatabaseContext for the DataModel project.
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            UpdateDatabase(app);

            app
                .UseLogging(Configuration, loggerFactory)
                .UseSwagger(Configuration)
                .UseConsul(lifetime)
                .UseCore();

            app.UseSubscribeAllEvents();
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DatabaseContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}

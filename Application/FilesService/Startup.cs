using Scion.FilesService.Contracts;
using Scion.FilesService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Scion.FilesService.Contracts.Sql;
using Scion.Infrastructure.Tenant;
using Scion.Datalayer.Contracts;
using Scion.Datalayer.Repositories;
using Scion.Datalayer.Context;

namespace FilesService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilesService", Version = "v1" });
            });

            //Add the factory.
            services.AddScoped<IResourceFactory, ResourceFactory>();

            //Add the SqlManager resolver -> TODO: Use strong typed object
            services.AddScoped<SqlManager>()
                    .AddScoped<IFileManager, SqlManager>(s =>
                    {
                        var service = s.GetService<SqlManager>();
                        service.Source = new SqlProvider();
                        Configuration.Bind("SqlProvider", service.Source);
                        return service;
                    });
            services.AddScoped<FileTableDbContext>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IFilesService, Scion.FilesService.Services.FilesService>();
            services.AddScoped<ITenantManager, TenantManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FilesService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

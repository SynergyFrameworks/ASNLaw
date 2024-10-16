
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scion.Infrastructure.ChangeLog;
using Scion.Infrastructure.Events;
using Scion.Infrastructure.ExportImport;
using Scion.Infrastructure.Localizations;
using Scion.Infrastructure.Notifications;
using Scion.Data.Common.ChangeLog;
using Scion.Data.CommonDynamicProperties;
using Scion.Data.Common.ExportImport;
using Scion.Data.CommonLocalizations;
using Scion.Data.Common.Repositories;
using Scion.Data.Common.Settings;
using Scion.Infrastructure.TransactionFileManager;
using Scion.Caching;
using System;
using Scion.Infrastructure.Bus;

using Scion.Data.CommonTransactionFileManager;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.ZipFile;
using System.IO.Abstractions;
using Scion.Data.Common.ZipFile;

namespace Scion.Data.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlatformServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PlatformDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Scion Analytics")));
            services.AddTransient<IPlatformRepository, PlatformRepository>();
            services.AddTransient<Func<IPlatformRepository>>(provider => () => provider.CreateScope().ServiceProvider.GetService<IPlatformRepository>());

            services.AddSettings();

            services.AddDynamicProperties();

            services.AddSingleton<InProcessBus>();
            services.AddSingleton<IHandlerRegistrar>(x => x.GetRequiredService<InProcessBus>());
            services.AddSingleton<IEventPublisher>(x => x.GetRequiredService<InProcessBus>());
            services.AddTransient<IChangeLogService, ChangeLogService>();
            services.AddTransient<ILastModifiedDateTime, ChangeLogService>();
            //services.AddTransient<ILastChangesService, LastChangesService>();

            services.AddTransient<IChangeLogSearchService, ChangeLogSearchService>();

            services.AddCaching(configuration);

            services.AddScoped<IPlatformExportImportManager, PlatformExportImportManager>();
            services.AddSingleton<ITransactionFileManager, TransactionFileManager.TransactionFileManager>();

            services.AddTransient<IEmailSender, DefaultEmailSender>();


            //Register dependencies for translation
            services.AddSingleton<ITranslationDataProvider, PlatformTranslationDataProvider>();
            services.AddSingleton<ITranslationDataProvider, ModulesTranslationDataProvider>();
            services.AddSingleton<ITranslationService, TranslationService>();

            services.AddSingleton<ICountriesService, FileSystemCountriesService>();
            services.AddSingleton<IFileSystem, FileSystem>();
            services.AddTransient<IZipFileWrapper, ZipFileWrapper>();

            return services;
        }
    }
}

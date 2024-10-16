using Infrastructure.Common.Assets;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Assets.FileSystem.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFileSystemBlobProvider(this IServiceCollection services, Action<FileSystemBlobOptions> setupAction = null)
        {
            services.AddSingleton<IBlobStorageProvider, FileSystemBlobProvider>();
            services.AddSingleton<IBlobUrlResolver, FileSystemBlobProvider>();
            if (setupAction != null)
            {
                services.Configure(setupAction);
            }
        }
    }
}

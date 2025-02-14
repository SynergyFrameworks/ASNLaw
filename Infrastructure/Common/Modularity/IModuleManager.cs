using System;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Modularity
{
    /// <summary>
    /// Defines the interface for the service that will retrieve and initialize the application's modules.
    /// </summary>
    public interface IModuleManager
    {
        /// <summary>
        /// Initializes the modules marked as <see cref="InitializationMode.WhenAvailable"/> on the <see cref="ModuleCatalog"/>.
        /// </summary>
        void Run();

        /// <summary>
        /// Loads and initializes the module on the <see cref="ModuleCatalog"/> with the name <paramref name="moduleName"/>.
        /// </summary>
        /// <param name="moduleName">Name of the module requested for initialization.</param>
        void LoadModule(string moduleName);

        /// <summary>
        /// Initializes the module during second iteration through all modules.
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="appBuilder"></param>
        void PostInitializeModule(ModuleInfo moduleInfo, IApplicationBuilder appBuilder);

        /// <summary>
        /// Raised repeatedly to provide progress as modules are downloaded.
        /// </summary>
        event EventHandler<ModuleDownloadProgressChangedEventArgs> ModuleDownloadProgressChanged;

        /// <summary>
        /// Raised when a module is loaded or fails to load.
        /// </summary>
        event EventHandler<LoadModuleCompletedEventArgs> LoadModuleCompleted;
    }
}

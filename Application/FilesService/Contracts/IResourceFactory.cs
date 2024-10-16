using Scion.Infrastructure.Tenant;

namespace Scion.FilesService.Contracts
{
    /// <summary>
    /// Client files resource implementation.
    /// </summary>
    public interface IResourceFactory
    {
        /// <summary>
        /// Gets the resource manager.
        /// </summary>
        /// <returns></returns>
        IFileManager GetFileManager();
    }
}

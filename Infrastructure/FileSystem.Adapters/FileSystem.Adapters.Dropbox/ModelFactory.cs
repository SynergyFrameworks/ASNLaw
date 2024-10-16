using Dropbox.Api.Files;
using StorageProvider.Models;

namespace StorageProvider.Adapters.Dropbox
{
    public static class ModelFactory
    {
        public static FileModel CreateFile(Metadata file)
        {
            return new FileModel
            {
                Name = file.Name,
                Path = file.PathDisplay,
                Length = (long) file.AsFile.Size,
                LastModifiedDateTime = file.AsFile.ServerModified
            };
        }

        public static DirectoryModel CreateDirectory(Metadata directory)
        {
            return new DirectoryModel
            {
                Name = directory.Name,
                Path = directory.PathDisplay
            };
        }
    }
}
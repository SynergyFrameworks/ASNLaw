using System.IO;

namespace StorageProvider.Models
{
    public class DirectoryModel : Model, IDirectory
    {
        public DirectoryModel()
        {
        }

        public DirectoryModel(DirectoryInfo directory)
        {
            Name = directory.Name;
            Path = directory.FullName;
            LastModifiedDateTime = directory.LastWriteTime;
            CreatedDateTime = directory.CreationTime;
        }
    }
}
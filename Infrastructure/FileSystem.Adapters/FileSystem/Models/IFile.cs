using System;

namespace StorageProvider.Models
{
    public interface IFile
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public long? Length { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public DateTime? CreatedDateTime { get; set; }
    }
}
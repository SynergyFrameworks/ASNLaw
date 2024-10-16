using Datalayer.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Datalayer.Contracts
{
    public interface IFileRepository
    {
        Task<Guid> AddFile(byte[] fileContent, FileDescription description);
        Task<bool> DeleteFile(Guid descriptionId);
        IEnumerable<FileDescription> GetAllFileDescriptions();
        Task<IEnumerable<FileDescription>> GetAllFileDescriptions(int pageNumber, int itemsPerPage);
        FileDescription GetFileDescription(Guid descriptionId);
        IEnumerable<FileDescription> GetFileDescriptions(ICollection<Guid> guids);
    }
}
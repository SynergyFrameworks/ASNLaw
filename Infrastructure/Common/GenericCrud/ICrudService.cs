using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Common;

namespace Infrastructure.GenericCrud
{
    /// <summary>
    /// Generic interface to use with CRUD services.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICrudService<T> where T : Entity
    {
        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<string> ids, string responseGroup = null);
        Task<T> GetByIdAsync(string id, string responseGroup = null);
        Task SaveChangesAsync(IEnumerable<T> models);
        Task DeleteAsync(IEnumerable<string> ids, bool softDelete = false);
    }
}

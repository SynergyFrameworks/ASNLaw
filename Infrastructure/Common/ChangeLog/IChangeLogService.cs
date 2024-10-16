using System.Threading.Tasks;

namespace Infrastructure.Common.ChangeLog
{
    public interface IChangeLogService
    {
        Task SaveChangesAsync(params OperationLog[] operationLogs);
        Task<OperationLog[]> GetByIdsAsync(string[] ids);
        Task DeleteAsync(string[] ids);
    }
}

using System.Threading.Tasks;

namespace Infrastructure.Common.ChangeLog
{
    public interface IChangeLogSearchService
    {
        Task<ChangeLogSearchResult> SearchAsync(ChangeLogSearchCriteria criteria);
    }
}

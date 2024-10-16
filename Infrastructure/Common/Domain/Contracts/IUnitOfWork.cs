using System.Threading.Tasks;

namespace Infrastructure.Common.Domain
{
    public interface IUnitOfWork
	{
		int Commit();
        Task<int> CommitAsync();
	}
}

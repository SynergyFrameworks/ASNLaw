using Infrastructure.Common.Persistence.Azure;
using System.Collections.Generic;

namespace Infrastructure.Common.Persistence
{
    public interface IDapperEntityManager : IEntityManager
    {
        void SetMapping(IDictionary<string, string> obj);

        IList<T> FindAll<T>(IList<TableQueryParameters> parameters = null, int startPage = 1, int pageSize = 20) where T : class;
        T FindSP<T>(string spName, IList<TableQueryParameters> parameters = null);
        IList<T> FindAllSP<T>(string spName, IList<TableQueryParameters> parameters = null); 


    }
}

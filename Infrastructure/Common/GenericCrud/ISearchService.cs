using System;
using System.Threading.Tasks;
using Infrastructure.Common;

namespace Infrastructure.GenericCrud
{
    /// <summary>
    /// Generic interface to use with search services
    /// </summary>
    /// <typeparam name="TCriteria"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    public interface ISearchService<TCriteria, TResult, TModel>
        where TCriteria : SearchCriteriaBase
        where TResult : GenericSearchResult<TModel>
        where TModel : Entity, ICloneable
    {
        Task<TResult> SearchAsync(TCriteria criteria);
    }
}

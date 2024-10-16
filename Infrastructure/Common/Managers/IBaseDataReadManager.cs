using Infrastructure.Common.Mapping;
using Infrastructure.Common.Persistence;
using Infrastructure.Query;
using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public interface IBaseDataReadManager
    {
        int GetListCount<T>(Criteria criteria = null);
        T GetTotals<T>(Criteria criteria = null);
        Dictionary<string, List<KeyValuePair<string, string>>> GetFilters<T>(Criteria criteria = null);
        Dictionary<string, List<KeyValuePair<string, string>>> GetFilters<T>(IList<T> results, Criteria criteria);

        int GetListCount(Type queryLookupType, Criteria criteria = null);
        IDictionary<string,object> GetDynamicTotals<T>(Criteria criteria = null);
        Dictionary<string, List<KeyValuePair<string, string>>> GetFilters(Type queryLookupType, Criteria criteria = null);
        Dictionary<string, List<KeyValuePair<string, string>>> GetFilters(Type queryLookupType, IList<IDictionary<string,object>> results, Criteria criteria);
        IList<IDynamicColumnValue> GetDynamicColumnHeaders<T>(Criteria criteria = null);
        DynamicResultSet RunPivotedQuery<T, R>(Criteria criteria, IEnumerable<string> removeColumns = null) where T : IPivotList;
    }
}

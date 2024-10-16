using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using Infrastructure.Common.Mapping;
using Infrastructure.Common.Persistence;
using Infrastructure.Common.Persistence.Dapper;
using Infrastructure.Query;
using Infrastructure.Common.Contracts;


namespace Infrastructure
{
    public abstract class BaseDataReadManager : IBaseDataReadManager
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BaseDataReadManager));
        private static IReadOnlyDataManager ReadOnlyDataManager { get { return new ReadOnlyDataManager(); } }

        public IList<IDynamicColumnValue> GetDynamicColumnHeaders<T>(Criteria criteria = null)
        {
            try
            {
                return ReadOnlyDataManager.GetDynamicColumnHeaders<T>(criteria);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error getting filters for type {0}: {1}", typeof(T).Name, ex);
                throw;
            }
        }

        public Dictionary<string, List<KeyValuePair<string, string>>> GetFilters<T>(Criteria criteria = null)
        {
            try
            {
                var filterTypeResultSet = ReadOnlyDataManager.GetFilterTypeResultSet<T>(criteria);
                return ReadOnlyDataManager.GetFilterTypes<T>(filterTypeResultSet, criteria);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error getting filters for type {0}: {1}", typeof(T).Name, ex);
                throw;
            }
        }

        public Dictionary<string, List<KeyValuePair<string, string>>> GetFilters<T>(IList<T> results, Criteria criteria)
        {
            try
            {
                return ReadOnlyDataManager.GetFilterTypes<T>(results, criteria);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error getting filters for type {0}: {1}", typeof(T).Name, ex);
                throw;
            }
        }

        public Dictionary<string, List<KeyValuePair<string, string>>> GetFilters(Type queryLookupType, Criteria criteria = null)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<KeyValuePair<string, string>>> GetFilters(Type queryLookupType, IList<IDictionary<string, object>> results, Criteria criteria)
        {
            throw new NotImplementedException();
        }

        public virtual int GetListCount<T>(Criteria criteria = null)
        {
            try
            {
                return ReadOnlyDataManager.GetCount<T>(criteria);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error getting count of list for type {0}: {1}", typeof(T).Name, ex);
                throw;
            }
        }

        public int GetListCount(Type queryLookupType, Criteria criteria = null)
        {
            try
            {
                var genericMethod = PrepareGenericMethod(queryLookupType, "GetCount");
                return (int)genericMethod.Invoke(null, new object[] { criteria });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error getting count of list for type {0}: {1}", queryLookupType.Name, ex);
                throw;
            }
        }

        public virtual T GetTotals<T>(Criteria criteria = null)
        {
            try
            {
                return ReadOnlyDataManager.GetTotals<T>(criteria);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error getting totals of list for type {0}: {1}", typeof(T).Name, ex);
                throw;
            }
        }

        public IDictionary<string, object> GetDynamicTotals<T>(Criteria criteria = null)
        {
            try
            {
                return (IDictionary<string, object>)ReadOnlyDataManager.DynamicTotal<T>(criteria);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error getting totals of list for type {0}: {1}", typeof(T).Name, ex);
                throw;
            }
        }

        private MethodInfo PrepareGenericMethod(Type queryLookupType, string methodName)
        {
            var readOnlyDataManagerType = ReadOnlyDataManager.GetType();
            var method = readOnlyDataManagerType.GetMethod("GetCount");
            return method.MakeGenericMethod(queryLookupType);
        }

        /// <summary>
        /// Execute a pivoted query.
        /// </summary>
        /// <typeparam name="T">Pivot Query Type</typeparam>
        /// <typeparam name="R">Primary Query Type</typeparam>
        /// <param name="criteria"></param>
        /// <param name="removeColumns">optional list of column names to remove. (for example if you need them there for sorting or soemthing but don't want to send out)</param>
        /// <returns></returns>
        public DynamicResultSet RunPivotedQuery<T, R>(Criteria criteria, IEnumerable<string> removeColumns = null) where T : IPivotList
        {
            try
            {
                var pivots = ReadOnlyDataManager.FindAll<T>(criteria);
                if (!pivots.Any())
                {
                    return null;
                }
                var pivotList = pivots.Select(f => f.Pivot).ToList();
                if (criteria.Parameters == null)
                {
                    criteria.Parameters = new SortedDictionary<string, object>();
                }
                criteria.Parameters.Add("pivotList", pivotList);
                criteria.DynamicHeaders = pivotList.Select(y => y.ToString()).ToList();
                var results = ReadOnlyDataManager.DynamicFindAll<R>(criteria);

                if(removeColumns != null)
                    foreach(var column in removeColumns)
                    {
                        var columnHeader = results.Metadata.FirstOrDefault(m => m.DisplayValue == column);
                        if (columnHeader != null)
                            results.Metadata.Remove(columnHeader);
                        foreach(var item in results.Data)
                        {
                            if (item.ContainsKey(column))
                                item.Remove(column);
                        }
                    }

                return results;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error finding {1}. {0}", ex, typeof(R).Name);
                throw;
            }
        }
    }
}

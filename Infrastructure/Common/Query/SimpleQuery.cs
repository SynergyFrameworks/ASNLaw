using Dapper;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Query
{

    public abstract class SimpleQuery<T> : BaseQuery<T>, ISimpleQuery
    {
        protected string query;

        public override QueryInfo GetQuery(Criteria criteria)
        {
            SetQuery();
            criteria = criteria ?? new Criteria();
            string formattedQuery = FilterAndSearchQuery(query, criteria);
            return new QueryInfo
            {
                Query = formattedQuery,
                Parameters = new DynamicParameters(criteria.Parameters)
            };
        }

        /// <summary>
        /// Override this to set the value of query. (The query string to be executed)
        /// </summary>
        public abstract void SetQuery();
    }


}


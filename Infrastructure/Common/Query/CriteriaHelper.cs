using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Infrastructure.Common.DataStructures;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Query
{
    public static class CriteriaHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CriteriaHelper));
        public static void DebugPrintCriteria(Criteria criteria)
        {
            if (criteria == null)
            {
                Log.Debug("No Criteria");
                return;
            }
            Log.DebugFormat("PageSize: {0}", criteria.PageSize);
            Log.DebugFormat("StartPage: {0}", criteria.StartPage);
            if (criteria.Parameters != null && criteria.Parameters.Any())
            {
                Log.Debug("Parameters:");
                foreach (var parameter in criteria.Parameters)
                {
                    Log.DebugFormat("{0}: {1}",parameter.Key, parameter.Value);
                }
            }
            else
            {
                Log.Debug("No Parameters.");
            }
            if (criteria.SortOrders != null && criteria.SortOrders.Any())
            {
                Log.Debug("Sort Orders:");
                foreach (var order in criteria.SortOrders.Collection)
                {
                    Log.DebugFormat("{0}: {1}", order.Key, order.Value);
                }
            }
            else
            {
                Log.Debug("No Sort Orders.");
            }
        }


        public static void AddInitalSort(Criteria criteria, string sortColumn, SortOrder sortOrder)
        {

            var initalSort = new SequentialDictionary<string, SortOrder>(sortColumn, sortOrder);


            foreach (KeyValuePair<string, SortOrder> pair in criteria.SortOrders.Collection)
            {
                if(pair.Key == sortColumn)
                    continue;
                initalSort.Add(pair.Key, pair.Value);
            }
            criteria.SortOrders = initalSort;
        }
    }
}

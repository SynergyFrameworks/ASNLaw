using System;
using System.Collections.Generic;
using log4net;
using Infrastructure.Common.Persistence;

using Infrastructure.Query.Reports;

namespace Infrastructure.Reporting
{
    public class ReportManager : IReportManager
    {
        public IReadOnlyDataManager ReadOnlyDataManager { get; set; }
        private static readonly ILog Log = LogManager.GetLogger(typeof(ReportManager));

        public IList<Report> GetReports(Criteria criteria)
        {
            try
            {
                return ReadOnlyDataManager.FindAll<Report>(criteria);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error finding Reports {0}", ex);
                throw;
            }
        }



        public IList<ReportCategory> GetReportCategories(Guid applicationId)
        {
            try
            {
                var criteria = new Criteria
                {
                    Parameters = new SortedDictionary<string, object>
                    {
                        {"applicationId", applicationId}
                    }
                };

                return ReadOnlyDataManager.FindAll<ReportCategory>(criteria);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error finding Reports {0}", ex);
                throw;
            }
        }

        public IList<PowerBIReport> GetPowerBIReports(Guid? externalReportId)
        {
            try
            {
                var criteria = new Criteria
                {
                    Parameters = new SortedDictionary<string, object>
                    {
                        {"externalReportId", externalReportId}
                    }
                };

                return ReadOnlyDataManager.FindAll<PowerBIReport>(criteria);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error finding PowerBI Reports {0}", ex);
                throw;
            }
        }
    }
}

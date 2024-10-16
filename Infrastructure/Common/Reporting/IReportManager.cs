using Infrastructure.Common.Persistence;

using Infrastructure.Query.Reports;
using System;
using System.Collections.Generic;

namespace Infrastructure.Reporting
{
    public interface IReportManager
    {
        IList<Report> GetReports(Criteria criteria);
        IList<ReportCategory> GetReportCategories(Guid applicationId);
        IList<PowerBIReport> GetPowerBIReports(Guid? externalReportId);
   
    }
}

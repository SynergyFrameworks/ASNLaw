using System.Collections.Generic;
using Infrastructure.Common.Domain.Performance;
using Infrastructure.Common.Domain.Reporting;

namespace Infrastructure.Reporting
{
    public interface IReportingUploadManager : IUploadDataManager
    {
        ReportingPeriod GetLatestReportingPeriod();
        IList<ReportingPeriod> GetReportingPeriods();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query.Reports
{
    public class PowerBIReport
    {
        public Guid ReportId { get; set; }
        public string ReportName { get; set; }
        public Guid? ExternalReportId { get; set; }
        public Guid? WorkspaceId { get; set; }
    }
}

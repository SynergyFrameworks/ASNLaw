using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Infrastructure.Query.Reports
{
    public class Report
    {
        public Guid ReportId { get; set; }
        public string ReportName { get; set; }
        public string ReportDescription { get; set; }
        public int ReportDisplayOrder { get; set; }

        public Guid ReportCategoryId { get; set; }
        public string ReportCategoryName { get; set; }
        public string ReportCategoryDescription { get; set; }
        public string ReportCategoryCssStyle { get; set; }
        public int ReportCategoryDisplayOrder { get; set; }

        public string RouteUri { get; set; }
        public Guid? ExternalReportId { get; set; }

        public IList<ReportParameter> Parameters
        {
            get
            {
                if (_parameters != null || ParameterJson == null)
                    return _parameters;

                _parameters = JsonConvert.DeserializeObject<IList<ReportParameter>>(ParameterJson);
                return _parameters;
            }
        }
        private IList<ReportParameter> _parameters;
        public string ParameterJson { get; set; }
    }
}

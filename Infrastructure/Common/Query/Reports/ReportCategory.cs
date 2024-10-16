using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Infrastructure.Query.Reports
{
    public class ReportCategory
    {
        public virtual Guid ReportCategoryId { get; set; }
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
        public virtual string CssStyle { get; set; }
        public virtual int DisplayOrder { get; set; }
        public IList<Report> Reports
        {
            get
            {
                if (_reports != null || ReportJson == null)
                    return _reports;

                _reports = JsonConvert.DeserializeObject<IList<Report>>(ReportJson);
                return _reports;
            }
        }
        private IList<Report> _reports;
        public string ReportJson { get; set; }
    }
}

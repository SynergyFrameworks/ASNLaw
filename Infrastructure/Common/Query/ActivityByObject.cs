using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query
{
    public class ActivityByObject
    {
        public string ActivityText { get; set; }
        public string ChangeReportJson { get; set; }
        public Guid ObjectId { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Guid Id { get; set; }
    }
}

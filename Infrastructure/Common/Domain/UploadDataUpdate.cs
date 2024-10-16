using System;
using System.Collections.Generic;

namespace Infrastructure.Common.Domain.Performance
{
    public class UploadDataUpdate
    {
        public string DataType { get; set; }
        public DateTime PeriodEndDate { get; set; }
        public Dictionary<string, object> AdditionalData { get; set; }
    }
}

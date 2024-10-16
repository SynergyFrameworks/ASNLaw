using System;
using System.Threading;
using Infrastructure.Common.Persistence.Azure;
using Infrastructure.Common.Extensions;
using Infrastructure.Common.Extensions;

namespace Infrastructure.Common.Domain.Reporting
{
    public class ReportingPeriod : PersistentEntity
    {
        private string _partitionKey;
        private string _rowKey;
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }

        public override string PartitionKey
        {
            get
            {
                if(string.IsNullOrEmpty(_partitionKey))
                    _partitionKey = Thread.CurrentThread.GetAssignedTenantId().ToString();
                return _partitionKey;
            }
            set { _partitionKey = value; }
        }

        public override string RowKey
        {
            get
            {
                if (string.IsNullOrEmpty(_rowKey))
                    _rowKey = PeriodEndDate.ToString("yyyy-MM-dd");
                return _rowKey;
            }
            set { _rowKey = value; }
        }
    }
}

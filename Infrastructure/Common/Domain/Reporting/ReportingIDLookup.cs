using System;
using System.Threading;
using Infrastructure.Common.Persistence.Azure;
using Infrastructure.Common.Extensions;
using Infrastructure.Common.Extensions;

namespace Infrastructure.Common.Domain.Reporting
{
    /// <summary>
    /// This is a temp workaround to handle using guids instead of string IDs - eventaully need to actually map back to DB guids
    /// </summary>
    public class ReportingIdLookup : PersistentEntity
    {       
        public Guid IdLookup { get; set; }

        public string TargetRowKey { get; set; }
        public string TargetPartitionKey { get; set; }
        public string DataType { get; set; }

        public override string PartitionKey
        {
            get { return Thread.CurrentThread.GetAssignedTenantId() + ":" + DataType; }
            set { }
        }

        public override string RowKey
        {
            get { return IdLookup.ToString(); }
            set
            {
                
            }
        }
    }
}

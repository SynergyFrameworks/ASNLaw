using System;
using Infrastructure.Common;

namespace Infrastructure.Common.ChangeLog
{
    public class ChangeLogSearchCriteria : SearchCriteriaBase
    {
        public EntryState[] OperationTypes { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

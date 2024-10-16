using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure
{
    public class BaseObject
    {
        public Guid Id { get; set; }
        public AuditUser CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public AuditUser LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsInactive { get; set; }
        public bool CanDelete { get; set; }
    }
}
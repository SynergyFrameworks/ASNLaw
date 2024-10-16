using System;

namespace Infrastructure.CQRS.Models
{
    public record SearchRange {
        
        public DateTimeOffset FromDate { get; set; } 
        
        public DateTimeOffset ToDate { get; set; }
    }
}

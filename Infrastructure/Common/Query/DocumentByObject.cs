using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Query
{
    public class DocumentByObject: DocumentByMetadata
    {
        public string FileName { get; set; }
        public string Description { get; set; }
        [ResultSetFilter]
        public string Category { get; set; }
        public Guid? CategoryId { get; set; }    
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }  
    }
}

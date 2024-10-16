using System.Collections.Generic;
using Infrastructure.Common;

namespace Infrastructure.ExportImport
{
    public class ExportImportProgressInfo : ValueObject
    {
        public ExportImportProgressInfo(string description = null)
        {
            Errors = new List<string>();
            Description = description;
        }

        public string Description { get; set; }
        public long TotalCount { get; set; }
        public long ProcessedCount { get; set; }
        public long ErrorCount
        {
            get
            {
                return Errors != null ? Errors.Count : 0;
            }
        }
        public ICollection<string> Errors { get; set; }

    }
}

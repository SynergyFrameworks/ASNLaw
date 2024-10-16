using System;
using System.IO;
using Infrastructure.Common.Persistence.Azure;
using Infrastructure.Common.Persistence.Azure.Transformers;
using System.Collections.Generic;

namespace Infrastructure.Common.Domain.Performance
{
    public class DataUpload:PersistentEntity
    {
        public string Name { get; set; }
        public Guid ApplicationId { get; set; }
        public string Description { get; set; }
        public string MimeType { get; set; }
        [IgnoreProperty]
        public byte[] Content { get; set; }
        [IgnoreProperty]
        public Stream Stream { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int NumberOfRecords { get; set; }
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }
        public string ParseTime { get; set; }
        public string UploadTime { get; set; }
        public string RemovalTime { get; set; }
        public string FileExtension { get; set; }

        public int HeaderRowNumber { get; set; }
        public int DataRowStartNumber { get; set; }

        [IgnoreProperty]
        public Dictionary<string,Stream> Streams { get; set; }
        
        [IgnoreProperty]
        public override string PartitionKey { get; set; }
        [IgnoreProperty]
        public override string RowKey { get; set; }

        public Guid? BlobFileId { get; set; }

        // In some cases files are not required to create a upload history entry when upload is failed
        [IgnoreProperty]
        public bool HasAttachment { get { return BlobFileId != null; } }
    }
}

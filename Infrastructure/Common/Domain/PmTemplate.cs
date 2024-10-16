using System;
using Infrastructure.Common.Persistence.Azure;
using Infrastructure.Common.Persistence.Azure.Transformers;

namespace Infrastructure.Common.Domain.Performance
{
    public class PmTemplate:PersistentEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string fileType { get; set; }
        [IgnoreProperty]
        public byte[] Content { get; set; }
        public Guid ApplicationId { get; set; }
        public string Description { get; set; }

        [IgnoreProperty]
        public override string PartitionKey { get; set; }

        [IgnoreProperty]
        public override string RowKey { get; set; }
        public string BlobFileId { get; set; }
        //public List<PmTemplateAdditionalData> AdditionalDataOptions { get; set; } 
    }
}

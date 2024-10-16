using Infrastructure.Common.Domain.Reference;
using System;

namespace Infrastructure.Common.Domain.Documents
{
    public class Document : BasePersistantObject
    {
        public virtual string MetaData { get; set; }
        public virtual byte[] Content { get; set; }
        public virtual Guid ContainerId { get; set; }
        public virtual string FileName { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string MimeType { get; set; }
        public virtual long Size { get; set; }
        public virtual DocumentCategory DocumentCategory { get; set; }
        public virtual Guid? ExternalId { get; set; }
        public virtual DateTime? DownloadedDate { get; set; } //by user themselves
        public virtual DateTime? AcceptedDate { get; set; }
        public virtual Guid? AcceptedBy { get; set; }
        public virtual string Url { get; set; }
        public virtual string FileType { get; set; }

    }
}

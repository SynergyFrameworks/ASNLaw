namespace Infrastructure.Common.Domain.Documents
{
    public class DocumentMetadata : BasePersistantObject
    {

        public virtual string Key { get; set; }
        public virtual string Value { get; set; }
        public virtual Document Document { get; set; }
    }
}

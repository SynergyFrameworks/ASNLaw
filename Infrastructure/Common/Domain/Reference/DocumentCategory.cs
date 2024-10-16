namespace Infrastructure.Common.Domain.Reference
{
    public class DocumentCategory : CodeDescription
    {
        public virtual string Type { get; set; }

        public DocumentCategory() { }
        public DocumentCategory(DocumentCategory documentCategory)
        {
            Code = documentCategory.Code;
            Description = documentCategory.Description;
            Id = documentCategory.Id;
            Type = documentCategory.Type;
            //IsInactive = documentCategory.IsInactive;
            DisplayOrder = documentCategory.DisplayOrder == null ? 0 : (int)documentCategory.DisplayOrder;
        }
    }
}

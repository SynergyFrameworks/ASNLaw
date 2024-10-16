using System;



namespace Infrastructure.Common.Domain
{
    public class CodeDescription : IUpdateObject
    {
        public virtual Guid Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }

       public string Application { get { return null; } set { } }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int? DisplayOrder { get; set; }
        public string Name {
            get { return Description; }
            set { }
        }
        public bool IsSearchDriven
        {
            get { return false; }
            set { }
        }
    }
}
using System;

namespace Infrastructure.Common.Domain.Settings
{
    public class MenuItem : BasePersistantObject
    {
        public virtual Guid Id { get; set; }
        public virtual String MenuId { get; set; }
        public virtual String Icon { get; set; }
        public virtual String Name { get; set; }
        public virtual String Path { get; set; }
        public virtual Boolean IsEnabled { get; set; }
        public virtual Boolean ShowInAppSettings { get; set; }
        public virtual Boolean ShowInSidebar { get; set; }
        public virtual int DisplayOrder { get; set; }
        public virtual Application Application { get; set; }
    }
}

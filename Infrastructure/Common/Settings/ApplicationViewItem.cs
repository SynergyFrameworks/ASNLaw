using Infrastructure.Common.Persistence;
using System;

namespace Infrastructure.Query.Settings
{
    public class ApplicationViewItem
    {
        public virtual Guid Id { get; set; }
        public virtual String Name { get; set; }
        public virtual String Value { get; set; }
    }
}

using Infrastructure.Common.Domain.Users;
using System;

namespace Infrastructure.Common.Domain.Settings
{
    public class Setting : BasePersistantObject
    {
        public virtual String Key { get; set; }
        public virtual String Value { get; set; }
        public virtual String Name { get; set; }
        public virtual String Description { get; set; }
        public virtual String DataType { get; set; }
        public virtual Application Application { get; set; }
        public virtual String ValueList { get; set; }
    }
}

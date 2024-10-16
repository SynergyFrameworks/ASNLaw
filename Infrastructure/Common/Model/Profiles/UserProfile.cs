using System.Collections.Generic;
using Infrastructure.Common;
using Infrastructure.Settings;
using System;

namespace Infrastructure.Model.Profiles
{
    public class UserProfile : Entity, IHasSettings
    {
        public Guid Id { get; set; }
        public virtual ICollection<ObjectSettingEntry> Settings { get; set; } = new List<ObjectSettingEntry>();
        public virtual string TypeName => GetType().Name;
    }
}

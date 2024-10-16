using System.Collections.Generic;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Settings;

namespace Scion.Infrastructure.Web.Model.Profiles
{
    public class UserProfile : Entity, IHasSettings
    {
        public virtual ICollection<ObjectSettingEntry> Settings { get; set; } = new List<ObjectSettingEntry>();
        public virtual string TypeName => GetType().Name;
    }
}

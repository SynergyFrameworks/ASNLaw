using System.Collections.Generic;
using Infrastructure.Common.Domain.Contracts;

namespace Infrastructure.Settings
{
    public interface IHasSettings : IEntity
    {
        string TypeName { get; }
        ICollection<ObjectSettingEntry> Settings { get; set; }
    }
}


using System;

namespace Infrastructure.Common.Domain.Settings
{
    public class UserSetting: Setting
    {
        public virtual Guid UserId { get; set; }
    }
}

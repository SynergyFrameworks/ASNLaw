using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Enums
{
   public enum OwnerType
    {
        Organization = 1,
        Client = 2,
        Group = 3,
        Unknown = -1
    }
}

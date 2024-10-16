using System.Collections.Generic;

namespace Scion.Infrastructure.Web.Model
{
    public class ChangedEntitiesResponse
    {
        public IList<ChangedEntity> Entities { get; set; }
    }
}

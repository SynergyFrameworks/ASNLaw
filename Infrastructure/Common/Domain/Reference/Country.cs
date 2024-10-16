using Infrastructure.Common;
using System.Collections.Generic;

namespace Infrastructure.Common.Domain.Reference
{
    public class Country
    { 
        public string Id { get; set; }

        public string Name { get; set; }

        public IList<CountryRegion> Regions { get; set; }
    }
}

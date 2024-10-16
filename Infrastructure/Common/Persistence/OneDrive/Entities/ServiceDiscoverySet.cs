using System.Collections.Generic;
using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    /// <summary>
    /// Office365 Service Discovery result with set of services being returned
    /// </summary>
    public class ServiceDiscoverySet
    {
        [JsonProperty("value")]
        public List<ServiceDiscoveryItem> Services { get; set; }
    }
}

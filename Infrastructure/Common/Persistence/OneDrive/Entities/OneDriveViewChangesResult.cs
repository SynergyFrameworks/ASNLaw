using Infrastructure.Common.Persistence.OneDrive.Enums;
using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    public class OneDriveViewChangesResult : OneDriveItemCollection
    {
        [JsonProperty("@changes.hasMoreChanges")]
        public bool HasMoreChanges { get; set; }

        [JsonProperty("@changes.token")]
        public string NextToken { get; set; }

        [JsonProperty("@changes.resync", DefaultValueHandling=DefaultValueHandling.IgnoreAndPopulate)]
        public OneDriveResyncLogicTypes ResyncBehavior { get; set; }
    }
}

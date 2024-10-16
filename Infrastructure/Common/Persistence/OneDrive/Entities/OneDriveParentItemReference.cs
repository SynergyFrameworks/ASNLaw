using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    public class OneDriveParentItemReference : OneDriveItemBase
    {
        [JsonProperty("parentReference")]
        public OneDriveItemReference ParentReference { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

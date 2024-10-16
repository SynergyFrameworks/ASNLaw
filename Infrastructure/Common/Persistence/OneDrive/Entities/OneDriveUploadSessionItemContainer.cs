using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    internal class OneDriveUploadSessionItemContainer : OneDriveItemBase
    {
        [JsonProperty("item", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public OneDriveUploadSessionItem Item { get; set; }
    }
}

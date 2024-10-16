using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    internal class GraphApiUploadSessionItemContainer : OneDriveItemBase
    {
        [JsonProperty("item", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public GraphApiUploadSessionItem Item { get; set; }
    }
}

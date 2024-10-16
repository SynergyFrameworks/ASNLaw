using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    public class OneDriveCollectionResponse<T> : OneDriveItemBase
    {
        [JsonProperty("value")]
        public T[] Collection { get; set; }

        [JsonProperty("@odata.nextLink")]
        public string NextLink { get; set; }
    }
}

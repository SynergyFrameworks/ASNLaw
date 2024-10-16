using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    internal class OneDriveCreateFolder : OneDriveItemBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("folder")]
        public object Folder { get; set; }
    }
}

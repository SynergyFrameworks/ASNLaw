using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    public class OneDriveSpecialFolderFacet
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

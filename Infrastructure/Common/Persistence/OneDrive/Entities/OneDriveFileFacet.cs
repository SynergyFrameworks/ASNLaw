using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    public class OneDriveFileFacet
    {
        [JsonProperty("hashes", DefaultValueHandling=DefaultValueHandling.Ignore)]
        public OneDriveHashesFacet Hashes { get; set; }

        [JsonProperty("mimeType", DefaultValueHandling=DefaultValueHandling.Ignore)]
        public string MimeType { get; set; }
    }
}

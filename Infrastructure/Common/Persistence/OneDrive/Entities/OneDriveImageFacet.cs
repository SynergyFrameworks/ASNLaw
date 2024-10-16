using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    public class OneDriveImageFacet
    {
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
    }
}

using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    public class OneDriveVideoFacet
    {
        [JsonProperty("bitrate")]
        public long Bitrate { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}

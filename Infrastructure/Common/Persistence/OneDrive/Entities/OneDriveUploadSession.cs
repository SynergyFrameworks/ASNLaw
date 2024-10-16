using System;
using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    public class OneDriveUploadSession : OneDriveItemBase
    {
        [JsonProperty("uploadUrl")]
        public string UploadUrl { get; set; }

        [JsonProperty("expirationDateTime")]
        public DateTimeOffset Expiration { get; set; }

        [JsonProperty("nextExpectedRanges")]
        public string[] ExpectedRanges { get; set; }
    }
}

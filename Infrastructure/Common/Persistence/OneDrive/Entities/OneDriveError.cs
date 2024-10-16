using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    public class OneDriveError : OneDriveItemBase
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
    }
}

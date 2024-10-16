using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    public abstract class OneDriveItemBase
    {
        /// <summary>
        /// The original raw JSON message
        /// </summary>
        [JsonIgnore]
        public string OriginalJson { get; set; }
    }
}

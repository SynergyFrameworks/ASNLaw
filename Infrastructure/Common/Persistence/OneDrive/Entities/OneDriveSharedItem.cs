using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    /// <summary>
    /// Represents an item that has been shared
    /// </summary>
    public class OneDriveSharedItem
    {
        /// <summary>
        /// The user account that owns the shared item
        /// </summary>
        [JsonProperty("owner")]
        public OneDriveIdentitySet Owner { get; set; }
    }
}

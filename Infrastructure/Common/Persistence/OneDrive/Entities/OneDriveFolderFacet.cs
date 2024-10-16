using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    public class OneDriveFolderFacet
    {
        [JsonProperty("childCount")]
        public long ChildCount { get; set; }
    }
}

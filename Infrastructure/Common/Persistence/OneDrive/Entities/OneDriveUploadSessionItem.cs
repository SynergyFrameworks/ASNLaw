using Infrastructure.Common.Persistence.OneDrive.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    internal class OneDriveUploadSessionItem
    {
        [JsonProperty("@name.conflictBehavior", DefaultValueHandling = DefaultValueHandling.Populate), JsonConverter(typeof(StringEnumConverter))]
        public NameConflictBehavior FilenameConflictBehavior { get; set; }

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Filename { get; set; }
    }
}

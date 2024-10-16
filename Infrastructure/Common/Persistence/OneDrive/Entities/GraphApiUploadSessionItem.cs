using Infrastructure.Common.Persistence.OneDrive.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Infrastructure.Common.Persistence.OneDrive
{
    internal class GraphApiUploadSessionItem
    {
        [JsonProperty("@microsoft.graph.conflictBehavior", DefaultValueHandling = DefaultValueHandling.Ignore), JsonConverter(typeof(StringEnumConverter))]
        public NameConflictBehavior FilenameConflictBehavior { get; set; }

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Filename { get; set; }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Infrastructure.Modularity
{
    public class ProgressMessage
    {
        public string Message { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ProgressMessageLevel Level { get; set; }
    }
}

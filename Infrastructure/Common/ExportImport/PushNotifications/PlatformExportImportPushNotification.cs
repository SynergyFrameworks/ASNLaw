using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Infrastructure.PushNotifications;

namespace Infrastructure.ExportImport.PushNotifications
{
    public abstract class PlatformExportImportPushNotification : PushNotification
    {
        protected PlatformExportImportPushNotification(string creator)
            : base(creator)
        {
            Errors = new List<string>();
        }

        [JsonProperty("jobId")]
        public string JobId { get; set; }
        [JsonProperty("finished")]
        public DateTime? Finished { get; set; }
        [JsonProperty("totalCount")]
        public long TotalCount { get; set; }
        [JsonProperty("processedCount")]
        public long ProcessedCount { get; set; }
        [JsonProperty("errorCount")]
        public long ErrorCount => Errors?.Count() ?? 0;
        [JsonProperty("errors")]
        public ICollection<string> Errors { get; set; }

    }
}

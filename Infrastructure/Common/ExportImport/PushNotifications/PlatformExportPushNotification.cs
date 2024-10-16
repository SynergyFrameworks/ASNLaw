using Newtonsoft.Json;

namespace Infrastructure.ExportImport.PushNotifications
{
    public class PlatformExportPushNotification : PlatformExportImportPushNotification
    {
        public PlatformExportPushNotification(string creator)
            : base(creator)
        {
        }

        [JsonProperty("downloadUrl")]
        public string DownloadUrl { get; set; }

    }
}

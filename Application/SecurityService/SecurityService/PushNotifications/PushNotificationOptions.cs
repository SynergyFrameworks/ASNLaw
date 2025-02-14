using System.ComponentModel.DataAnnotations;

namespace Scion.Infrastructure.Web.PushNotifications
{
    public class PushNotificationOptions
    {
        public string ScalabilityMode { get; set; } = "None";
        [Url, Required]
        public string HubUrl { get; set; }
        public bool ForceWebSockets { get; set; } = false;
    }
}

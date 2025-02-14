using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Infrastructure.PushNotifications;

namespace Infrastructure.Modularity.PushNotifications
{
    public class ModulePushNotification : PushNotification
    {
        public ModulePushNotification(string creator)
            : base(creator)
        {
            ProgressLog = new List<ProgressMessage>();
        }

        [JsonProperty("started")]
        public DateTime? Started { get; set; }

        [JsonProperty("finished")]
        public DateTime? Finished { get; set; }

        [JsonProperty("progressLog")]
        public ICollection<ProgressMessage> ProgressLog { get; set; }
    }
}

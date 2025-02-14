﻿using Newtonsoft.Json;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    public class OneDriveItemReference : OneDriveItemBase
    {
        [JsonProperty("driveId")]
        public string DriveId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}

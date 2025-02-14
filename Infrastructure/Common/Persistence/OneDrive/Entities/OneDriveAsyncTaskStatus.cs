﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    public class OneDriveAsyncTaskStatus : OneDriveItemBase
    {
        [JsonProperty("operation"), JsonConverter(typeof(StringEnumConverter))]
        public Enums.OneDriveAsyncJobType Operation { get; set; }

        [JsonProperty("percentageComplete")]
        public double PercentComplete { get; set; }

        [JsonProperty("status"), JsonConverter(typeof(StringEnumConverter))]
        public Enums.OneDriveAsyncJobStatus Status { get; set; }
        
    }
}

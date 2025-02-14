﻿using Infrastructure.Common.Persistence.OneDrive.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{
    public class OneDriveSharingLinkFacet
    {
        /// <summary>
        /// Url to access the item on which the permissions are applied
        /// </summary>
        [JsonProperty("webUrl")]
        public string WebUrl { get; set; }

        [JsonProperty("application")]
        public OneDriveIdentity Application { get; set; }

        /// <summary>
        /// Type of rights assigned to this item
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public OneDriveLinkType Type { get; set; }
    }
}

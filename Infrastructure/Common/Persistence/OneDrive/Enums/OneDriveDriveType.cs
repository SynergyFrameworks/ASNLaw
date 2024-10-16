﻿using System.Runtime.Serialization;

namespace Infrastructure.Common.Persistence.OneDrive.Enums
{
    /// <summary>
    /// Types of OneDrive
    /// </summary>
    public enum OneDriveDriveType
    {
        /// <summary>
        /// Consumer OneDrive
        /// </summary>
        [EnumMember(Value = "personal")] 
        Personal,
        
        /// <summary>
        /// OneDrive for Business
        /// </summary>
        [EnumMember(Value = "business")]
        Business,

        /// <summary>
        /// SharePoint Document Library
        /// </summary>
        [EnumMember(Value = "documentLibrary")]
        SharePointDocumentLibrary
    }
}
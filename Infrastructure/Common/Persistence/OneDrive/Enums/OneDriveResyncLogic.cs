using System.Runtime.Serialization;

namespace Infrastructure.Common.Persistence.OneDrive.Enums
{
    public enum OneDriveResyncLogicTypes
    {
        [EnumMember(Value = "none")]
        NoResyncRequired = 0,

        [EnumMember(Value = "ResetCacheAndFullSync")]
        ResetCacheAndFullSync,

        [EnumMember(Value = "FullSyncAndUploadDifferences")]
        FullSyncAndUploadDifferences
    }
}

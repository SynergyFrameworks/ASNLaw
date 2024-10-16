using System.Runtime.Serialization;

namespace Infrastructure.Common.Persistence.OneDrive.Enums
{
    public enum OneDriveAsyncJobType
    {
        [EnumMember(Value = "DownloadUrl")]
        DownloadUrl,
        [EnumMember(Value = "CopyItem")]
        CopyItem
    }
}

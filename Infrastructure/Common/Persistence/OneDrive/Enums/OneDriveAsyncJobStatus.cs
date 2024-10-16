using System.Runtime.Serialization;

namespace Infrastructure.Common.Persistence.OneDrive.Enums
{
    public enum OneDriveAsyncJobStatus
    {
        [EnumMember(Value = "NotStarted")]
        NotStarted,

        [EnumMember(Value = "InProgress")]
        InProgress,

        [EnumMember(Value = "Complete")]
        Complete
    }
}

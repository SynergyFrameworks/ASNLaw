using System.Runtime.Serialization;

namespace Infrastructure.Common.Persistence.OneDrive.Enums
{
    public enum NameConflictBehavior
    {
        [EnumMember(Value = "fail")]
        Fail,

        [EnumMember(Value = "replace")]
        Replace,

        [EnumMember(Value = "rename")]
        Rename
    }
}

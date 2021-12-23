using System.Runtime.Serialization;

namespace FitnessManager.Domain.Common.Enums
{
    public enum EUserRole
    {
        [EnumMember(Value = "Admin")]
        Admin = 0,
        
        [EnumMember(Value = "RegularUser")]
        RegularUser = 1,
    }
}
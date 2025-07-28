using System.Runtime.Serialization;

namespace DevsuTestApi.Enums;

public enum Status
{
    [EnumMember(Value = "Activo")]
    Active = 1,
    
    [EnumMember(Value = "Inactivo")]
    Inactive
}
using System.Runtime.Serialization;

namespace DevsuApi.Domain.Enums;

public enum Status
{
    [EnumMember(Value = "Activo")]
    Active = 1,
    
    [EnumMember(Value = "Inactivo")]
    Inactive
}
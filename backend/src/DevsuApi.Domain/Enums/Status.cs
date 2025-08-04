using System.Runtime.Serialization;

namespace DevsuApi.Domain.Enums;

public enum Status
{
    [EnumMember(Value = "Activo")]
    Active,
    
    [EnumMember(Value = "Inactivo")]
    Inactive
}
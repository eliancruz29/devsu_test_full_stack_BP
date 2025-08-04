using System.Runtime.Serialization;

namespace DevsuApi.Domain.Enums;

public enum AccountTypes
{
    [EnumMember(Value = "Ahorro")]
    Savings,
    
    [EnumMember(Value = "Corriente")]
    Checks
}
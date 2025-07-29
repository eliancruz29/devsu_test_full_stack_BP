using System.Runtime.Serialization;

namespace DevsuApi.Domain.Enums;

public enum AccountTypes
{
    [EnumMember(Value = "Ahorro")]
    Savings = 1,
    
    [EnumMember(Value = "Corriente")]
    Checks
}
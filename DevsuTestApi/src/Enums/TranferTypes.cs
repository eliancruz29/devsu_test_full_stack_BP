using System.Runtime.Serialization;

namespace DevsuTestApi.Enums;

public enum TranferTypes
{
    [EnumMember(Value = "Credito")]
    Credit = 1,
    
    [EnumMember(Value = "Debito")]
    Debit
}
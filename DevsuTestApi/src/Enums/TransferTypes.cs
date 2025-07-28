using System.Runtime.Serialization;

namespace DevsuTestApi.Enums;

public enum TransferTypes
{
    [EnumMember(Value = "Credito")]
    Credit = 1,
    
    [EnumMember(Value = "Debito")]
    Debit
}
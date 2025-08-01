using System.Runtime.Serialization;

namespace DevsuApi.Domain.Enums;

public enum TransferTypes
{
    [EnumMember(Value = "Credito")]
    Credit = 1,
    
    [EnumMember(Value = "Debito")]
    Debit
}
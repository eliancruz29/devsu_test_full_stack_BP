using System.Runtime.Serialization;

namespace DevsuApi.Domain.Enums;

public enum TransferTypes
{
    [EnumMember(Value = "Credito")]
    Credit,
    
    [EnumMember(Value = "Debito")]
    Debit
}
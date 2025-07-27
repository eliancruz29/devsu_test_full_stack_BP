using DevsuTestApi.Enums;

namespace DevsuTestApi.Entities;

public class Account : BaseEntity
{
    public Guid Id { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public AccountTypes Type { get; set; }

    public int OpeningBalance { get; set; }

    public Status Status { get; set; }
}
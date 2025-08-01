using DevsuApi.Domain.Enums;

namespace DevsuApi.Features.Accounts.Shared;

public class AccountResponse
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public AccountTypes Type { get; set; }
    public int OpeningBalance { get; set; }
    public Status Status { get; set; }
}

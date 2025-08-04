using DevsuApi.Domain.Enums;
using DevsuApi.Infrastructure.Extensions;

namespace DevsuApi.Features.Accounts.Shared;

public class AccountResponse
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public AccountTypes Type { get; set; }
    public string TypeName => Type.GetDatabaseName();
    public int OpeningBalance { get; set; }
    public Status Status { get; set; }
    public string StatusName => Status.GetDatabaseName();
}

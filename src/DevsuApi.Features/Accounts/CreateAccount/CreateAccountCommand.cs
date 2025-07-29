using DevsuApi.Domain.Enums;
using DevsuApi.Domain.Shared;
using MediatR;

namespace DevsuApi.Features.Accounts.CreateAccount;

public sealed class CreateAccountCommand : IRequest<Result<Guid>>
{
    public Guid ClientId { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public AccountTypes Type { get; set; }

    public int OpeningBalance { get; set; }

    public Status Status { get; set; }
}

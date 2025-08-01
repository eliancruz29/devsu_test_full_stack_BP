using DevsuApi.Domain.Enums;
using DevsuApi.Domain.Shared;
using DevsuApi.Features.Accounts.Shared;
using MediatR;

namespace DevsuApi.Features.Accounts.CreateAccount;

public sealed class CreateAccountCommand : BaseAccountCommand, IRequest<Result<Guid>>
{
    public Guid ClientId { get; set; }

    public AccountTypes Type { get; set; }
}

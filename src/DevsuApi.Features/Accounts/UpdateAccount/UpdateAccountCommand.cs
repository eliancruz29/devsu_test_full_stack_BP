using DevsuApi.Domain.Enums;
using DevsuApi.Features.Accounts.Shared;
using DevsuApi.Domain.Shared;
using MediatR;

namespace DevsuApi.Features.Accounts.UpdateAccount;

public sealed class UpdateAccountCommand : BaseAccountCommand, IRequest<Result<AccountResponse>>
{
    public Guid Id { get; set; }
    public AccountTypes Type { get; set; }
    public Status Status { get; set; }
}

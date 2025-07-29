using DevsuApi.Features.Accounts.Shared;
using DevsuApi.Domain.Shared;
using MediatR;

namespace DevsuApi.Features.Accounts.GetAccount;

public sealed class GetAccountQuery : IRequest<Result<AccountResponse>>
{
    public Guid Id { get; set; }
}

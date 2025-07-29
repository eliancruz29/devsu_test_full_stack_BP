using DevsuApi.Features.Accounts.Shared;
using DevsuApi.Domain.Shared;
using MediatR;

namespace DevsuApi.Features.Accounts.GetListOfAccounts;

public sealed class GetListOfAccountsQuery : IRequest<Result<List<AccountResponse>>>
{
    public Guid? ClientId { get; set; }
}

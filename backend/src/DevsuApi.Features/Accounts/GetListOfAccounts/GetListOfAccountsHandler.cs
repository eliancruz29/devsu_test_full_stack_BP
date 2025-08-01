using DevsuApi.Features.Accounts.Shared;
using DevsuApi.Domain.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DevsuApi.Domain.Repositories;

namespace DevsuApi.Features.Accounts.GetListOfAccounts;

public sealed class GetListOfAccountsHandler(IAccountRepository accountRepository) : IRequestHandler<GetListOfAccountsQuery, Result<List<AccountResponse>>>
{
    public async Task<Result<List<AccountResponse>>> Handle(GetListOfAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await accountRepository.GetAll()
            .Where(a => !request.ClientId.HasValue || a.ClientId == request.ClientId)
            .ProjectToType<AccountResponse>()
            .ToListAsync(cancellationToken);

        if (accounts is null)
        {
            return Result.Failure<List<AccountResponse>>(new Error(
                "GetListOfAccounts.Null",
                "The list accounts was not found"));
        }

        return Result.Success(accounts);
    }
}

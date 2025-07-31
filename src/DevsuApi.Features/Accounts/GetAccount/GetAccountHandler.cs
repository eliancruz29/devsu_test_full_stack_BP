using DevsuApi.Domain.Repositories;
using DevsuApi.Domain.Shared;
using DevsuApi.Features.Accounts.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevsuApi.Features.Accounts.GetAccount;

public sealed class GetAccountHandler(IAccountRepository accountRepository) : IRequestHandler<GetAccountQuery, Result<AccountResponse>>
{
    public async Task<Result<AccountResponse>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        AccountResponse? account = await accountRepository.GetById(request.Id)
            .ProjectToType<AccountResponse>()
            .SingleOrDefaultAsync(cancellationToken);

        if (account is null)
        {
            return Result.Failure<AccountResponse>(new Error(
                "GetAccount.Null",
                "The account with the specified ID was not found"));
        }

        return account;
    }
}

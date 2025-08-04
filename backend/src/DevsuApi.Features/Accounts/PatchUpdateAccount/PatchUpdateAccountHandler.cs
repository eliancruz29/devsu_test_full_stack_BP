using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Interfaces;
using DevsuApi.Domain.Repositories;
using DevsuApi.Domain.Shared;
using DevsuApi.Features.Accounts.Shared;
using FluentValidation;
using Mapster;
using MediatR;

namespace DevsuApi.Features.Accounts.PatchUpdateAccount;

public sealed class PatchUpdateAccountHandler(
    IAccountRepository accountRepository,
    IUnitOfWork unitOfWork,
    IValidator<PatchUpdateAccountCommand> validator) : IRequestHandler<PatchUpdateAccountCommand, Result<AccountResponse>>
{
    public async Task<Result<AccountResponse>> Handle(PatchUpdateAccountCommand request, CancellationToken cancellationToken)
    {
        Account? existantAccount = await accountRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existantAccount is null)
        {
            return Result.Failure<AccountResponse>(new Error(
                "PatchUpdateAccount.Null",
                "The account with the specified ID was not found"));
        }

        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return Result.Failure<AccountResponse>(new Error(
                "PatchUpdateAccount.Validation",
                validationResult.ToString()));
        }

        existantAccount.Update(
            string.IsNullOrWhiteSpace(request.AccountNumber) ? existantAccount.AccountNumber : request.AccountNumber,
            request.Type ?? existantAccount.Type,
            request.OpeningBalance ?? existantAccount.OpeningBalance,
            request.Status ?? existantAccount.Status
        );

        accountRepository.Update(existantAccount);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return existantAccount.Adapt<AccountResponse>();
    }
}

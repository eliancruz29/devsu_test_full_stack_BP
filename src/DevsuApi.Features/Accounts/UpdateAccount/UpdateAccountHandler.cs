using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Interfaces;
using DevsuApi.Domain.Repositories;
using DevsuApi.Domain.Shared;
using DevsuApi.Features.Accounts.Shared;
using FluentValidation;
using Mapster;
using MediatR;

namespace DevsuApi.Features.Accounts.UpdateAccount;

public sealed class UpdateAccountHandler(
    IAccountRepository accountRepository,
    IUnitOfWork unitOfWork,
    IValidator<UpdateAccountCommand> validator) : IRequestHandler<UpdateAccountCommand, Result<AccountResponse>>
{
    public async Task<Result<AccountResponse>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        Account? existantAccount = await accountRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existantAccount is null)
        {
            return Result.Failure<AccountResponse>(new Error(
                "UpdateAccount.Null",
                "The account with the specified ID was not found"));
        }

        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return Result.Failure<AccountResponse>(new Error(
                "UpdateAccount.Validation",
                validationResult.ToString()));
        }

        existantAccount.Update(
            request.AccountNumber,
            request.Type,
            request.OpeningBalance,
            request.Status);

        accountRepository.Update(existantAccount);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return existantAccount.Adapt<AccountResponse>();
    }
}

using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Interfaces;
using DevsuApi.Domain.Repositories;
using DevsuApi.Domain.Shared;
using FluentValidation;
using MediatR;

namespace DevsuApi.Features.Accounts.CreateAccount;

public sealed class CreateAccountHandler(
    IAccountRepository accountRepository,
    IClientRepository clientRepository,
    IUnitOfWork unitOfWork,
    IValidator<CreateAccountCommand> validator) : IRequestHandler<CreateAccountCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return Result.Failure<Guid>(new Error(
                "CreateAccount.Validation",
                validationResult.ToString()));
        }

        Client? client = await clientRepository.GetByIdWithAccountsAsync(request.ClientId, cancellationToken);
        if (client is null)
        {
            return Result.Failure<Guid>(new Error(
                "CreateAccount.ClientNotFound",
                "The specified client does not exist."));
        }

        Account newAccount = client.AddAccount(
            request.AccountNumber,
            request.Type,
            request.OpeningBalance);

        accountRepository.Add(newAccount);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return newAccount.Id;
    }
}

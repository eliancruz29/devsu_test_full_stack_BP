using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Interfaces;
using DevsuApi.Domain.Repositories;
using DevsuApi.Domain.Shared;
using FluentValidation;
using MediatR;

namespace DevsuApi.Features.Transfers.CreateTransfer;

public sealed class CreateTransferHandler(
    IAccountRepository accountRepository,
    IUnitOfWork unitOfWork,
    IValidator<CreateTransferCommand> validator) : IRequestHandler<CreateTransferCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return Result.Failure<Guid>(new Error(
                "CreateTransfer.Validation",
                validationResult.ToString()));
        }

        Account? account = await accountRepository.GetByIdWithTransfersAsync(request.AccountId, cancellationToken);
        if (account is null)
        {
            return Result.Failure<Guid>(new Error(
                "CreateTransfer.AccountNotFound",
                "The specified account does not exist."));
        }

        Transfer newTransfer = account.AddTransfer(request.Type, request.Amount);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return newTransfer.Id;
    }
}

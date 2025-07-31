using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Interfaces;
using DevsuApi.Domain.Repositories;
using DevsuApi.Domain.Shared;
using DevsuApi.Features.Transfers.Shared;
using FluentValidation;
using Mapster;
using MediatR;

namespace DevsuApi.Features.Transfers.UpdateTransfer;

public sealed class UpdateTransferHandler(
    ITransferRepository transferRepository,
    IUnitOfWork unitOfWork,
    IValidator<UpdateTransferCommand> validator) : IRequestHandler<UpdateTransferCommand, Result<TransferResponse>>
{
    public async Task<Result<TransferResponse>> Handle(UpdateTransferCommand request, CancellationToken cancellationToken)
    {
        Transfer? existantTransfer = await transferRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existantTransfer is null)
        {
            return Result.Failure<TransferResponse>(new Error(
                "UpdateTransfer.Null",
                "The transfer with the specified ID was not found"));
        }

        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return Result.Failure<TransferResponse>(new Error(
                "UpdateTransfer.Validation",
                validationResult.ToString()));
        }

        existantTransfer.Update(
            request.Type,
            request.Amount);

        transferRepository.Update(existantTransfer);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return existantTransfer.Adapt<TransferResponse>();
    }
}

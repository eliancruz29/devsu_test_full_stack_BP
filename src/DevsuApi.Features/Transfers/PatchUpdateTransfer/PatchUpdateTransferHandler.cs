using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Interfaces;
using DevsuApi.Domain.Repositories;
using DevsuApi.Domain.Shared;
using DevsuApi.Features.Transfers.Shared;
using FluentValidation;
using Mapster;
using MediatR;

namespace DevsuApi.Features.Transfers.PatchUpdateTransfer;

public sealed class PatchUpdateTransferHandler(
    ITransferRepository transferRepository,
    IUnitOfWork unitOfWork,
    IValidator<PatchUpdateTransferCommand> validator) : IRequestHandler<PatchUpdateTransferCommand, Result<TransferResponse>>
{
    public async Task<Result<TransferResponse>> Handle(PatchUpdateTransferCommand request, CancellationToken cancellationToken)
    {
        Transfer? existantTransfer = await transferRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existantTransfer is null)
        {
            return Result.Failure<TransferResponse>(new Error(
                "PatchUpdateTransfer.Null",
                "The transfer with the specified ID was not found"));
        }

        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return Result.Failure<TransferResponse>(new Error(
                "PatchUpdateTransfer.Validation",
                validationResult.ToString()));
        }

        existantTransfer.Update(
            request.Type ?? existantTransfer.Type,
            request.Amount ?? existantTransfer.Amount
        );

        transferRepository.Update(existantTransfer);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return existantTransfer.Adapt<TransferResponse>();
    }
}

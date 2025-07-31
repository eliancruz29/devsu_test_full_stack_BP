using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Exceptions.Transfers;
using DevsuApi.Domain.Interfaces;
using DevsuApi.Domain.Repositories;
using MediatR;

namespace DevsuApi.Features.Transfers.DeleteTransfer;

public sealed class DeleteTransferHandler(
    ITransferRepository transferRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteTransferQuery>
{
    public async Task Handle(DeleteTransferQuery request, CancellationToken cancellationToken)
    {
        Transfer? existantTransfer = await transferRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existantTransfer is null)
        {
            throw new TransferNotFoundException(request.Id);
        }

        transferRepository.Remove(existantTransfer);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

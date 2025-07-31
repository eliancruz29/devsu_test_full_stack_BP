using DevsuApi.Domain.Repositories;
using DevsuApi.Domain.Shared;
using DevsuApi.Features.Transfers.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevsuApi.Features.Transfers.GetTransfer;

public sealed class GetTransferHandler(ITransferRepository transferRepository) : IRequestHandler<GetTransferQuery, Result<TransferResponse>>
{
    public async Task<Result<TransferResponse>> Handle(GetTransferQuery request, CancellationToken cancellationToken)
    {
        TransferResponse? transfer = await transferRepository.GetById(request.Id)
            .ProjectToType<TransferResponse>()
            .SingleOrDefaultAsync(cancellationToken);

        if (transfer is null)
        {
            return Result.Failure<TransferResponse>(new Error(
                "GetTransfer.Null",
                "The transfer with the specified ID was not found"));
        }

        return transfer;
    }
}

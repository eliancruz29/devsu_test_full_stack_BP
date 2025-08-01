using DevsuApi.Features.Transfers.Shared;
using DevsuApi.Domain.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DevsuApi.Domain.Repositories;

namespace DevsuApi.Features.Transfers.GetListOfTransfers;

public sealed class GetListOfTransfersHandler(ITransferRepository transferRepository) : IRequestHandler<GetListOfTransfersQuery, Result<List<TransferResponse>>>
{
    public async Task<Result<List<TransferResponse>>> Handle(GetListOfTransfersQuery request, CancellationToken cancellationToken)
    {
        var transfers = await transferRepository.GetAll()
            .Where(t => !request.AccountId.HasValue || t.AccountId == request.AccountId)
            .ProjectToType<TransferResponse>()
            .ToListAsync(cancellationToken);

        if (transfers is null)
        {
            return Result.Failure<List<TransferResponse>>(new Error(
                "GetListOfTransfers.Null",
                "The list transfers was not found"));
        }

        return Result.Success(transfers);
    }
}

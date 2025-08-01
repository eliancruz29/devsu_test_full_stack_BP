using DevsuApi.Features.Transfers.Shared;
using DevsuApi.Domain.Shared;
using MediatR;

namespace DevsuApi.Features.Transfers.GetListOfTransfers;

public sealed class GetListOfTransfersQuery : IRequest<Result<List<TransferResponse>>>
{
    public Guid? AccountId { get; set; }
}

using DevsuApi.Domain.Shared;
using DevsuApi.Features.Transfers.Shared;
using MediatR;

namespace DevsuApi.Features.Transfers.GetTransfer;

public sealed class GetTransferQuery : IRequest<Result<TransferResponse>>
{
    public Guid Id { get; set; }
}

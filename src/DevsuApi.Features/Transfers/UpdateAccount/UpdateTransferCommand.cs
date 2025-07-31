using DevsuApi.Domain.Shared;
using DevsuApi.Features.Transfers.Shared;
using MediatR;

namespace DevsuApi.Features.Transfers.UpdateTransfer;

public sealed class UpdateTransferCommand : BaseTransferCommand, IRequest<Result<TransferResponse>>
{
    public Guid Id { get; set; }
}

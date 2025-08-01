using DevsuApi.Domain.Enums;
using DevsuApi.Features.Transfers.Shared;
using DevsuApi.Domain.Shared;
using MediatR;

namespace DevsuApi.Features.Transfers.PatchUpdateTransfer;

public class PatchUpdateTransferCommand : IRequest<Result<TransferResponse>>
{
    public Guid Id { get; set; }
    public TransferTypes? Type { get; set; }
    public int? Amount { get; set; }
}

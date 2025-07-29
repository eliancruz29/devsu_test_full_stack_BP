using DevsuApi.Domain.Enums;
using DevsuApi.Domain.Shared;
using MediatR;

namespace DevsuApi.Features.Transfers.CreateTransfer;

public sealed class CreateTransferCommand : IRequest<Result<Guid>>
{
    public Guid AccountId { get; set; }

    public TransferTypes Type { get; set; }

    public int Amount { get; set; }
}

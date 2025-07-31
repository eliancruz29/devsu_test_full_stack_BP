using DevsuApi.Domain.Shared;
using DevsuApi.Features.Transfers.Shared;
using MediatR;

namespace DevsuApi.Features.Transfers.CreateTransfer;

public sealed class CreateTransferCommand : BaseTransferCommand, IRequest<Result<Guid>>
{
    public Guid AccountId { get; set; }
}

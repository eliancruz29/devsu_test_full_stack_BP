using MediatR;

namespace DevsuApi.Features.Transfers.DeleteTransfer;

public sealed class DeleteTransferQuery : IRequest
{
    public Guid Id { get; set; }
}

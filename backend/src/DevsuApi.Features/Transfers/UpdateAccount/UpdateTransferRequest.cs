using DevsuApi.Domain.Enums;

namespace DevsuApi.Features.Transfers.UpdateTransfer;

public record UpdateTransferRequest
(
    Guid Id,
    TransferTypes Type,
    int Amount
);

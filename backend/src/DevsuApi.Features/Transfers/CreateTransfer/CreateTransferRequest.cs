using DevsuApi.Domain.Enums;

namespace DevsuApi.Features.Transfers.CreateTransfer;

public record CreateTransferRequest
(
    Guid AccountId,
    TransferTypes Type,
    int Amount
);

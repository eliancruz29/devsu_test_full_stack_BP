using DevsuApi.Domain.Enums;

namespace DevsuApi.Features.Transfers.PatchUpdateTransfer;

public record PatchUpdateTransferRequest
(
    Guid Id,
    TransferTypes? Type,
    int? Amount
);

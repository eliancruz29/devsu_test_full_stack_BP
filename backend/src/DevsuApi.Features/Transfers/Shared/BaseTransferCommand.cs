using DevsuApi.Domain.Enums;

namespace DevsuApi.Features.Transfers.Shared;

public class BaseTransferCommand
{
    public TransferTypes Type { get; set; }
    public int Amount { get; set; }
}

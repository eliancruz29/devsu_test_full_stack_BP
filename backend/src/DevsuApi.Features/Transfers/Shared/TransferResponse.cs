using DevsuApi.Domain.Enums;

namespace DevsuApi.Features.Transfers.Shared;

public class TransferResponse
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public DateTime Date { get; set; }
    public TransferTypes Type { get; set; }
    public int Amount { get; set; }
    public int Balance { get; set; }
}

using DevsuApi.Domain.Enums;
using DevsuApi.Infrastructure.Extensions;

namespace DevsuApi.Features.Transfers.Shared;

public class TransferResponse
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public DateTime Date { get; set; }
    public TransferTypes Type { get; set; }
    public string TypeName => Type.GetDatabaseName();
    public int Amount { get; set; }
    public int Balance { get; set; }
    public Status Status { get; set; }
    public string StatusName => Status.GetDatabaseName();
}

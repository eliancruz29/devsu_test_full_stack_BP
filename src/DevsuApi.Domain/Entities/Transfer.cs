using DevsuApi.Domain.Enums;
using DevsuApi.SharedKernel;

namespace DevsuApi.Domain.Entities;

public sealed class Transfer : BaseEntity
{
    private Transfer(
        Guid id,
        Guid accountId,
        DateTime date,
        TransferTypes type,
        int amount,
        int balance
    ) : base(id)
    {
        AccountId = accountId;
        Date = date;
        Type = type;
        Amount = amount;
        Balance = balance;
    }

    public Guid AccountId { get; private set; }

    public DateTime Date { get; private set; }

    public TransferTypes Type { get; private set; }

    public int Amount { get; private set; }

    public int Balance { get; private set; }
    
    public static Transfer Create(
        Guid accountId,
        TransferTypes type,
        int amount,
        int balance)
    {
        return new (
            Guid.NewGuid(),
            accountId,
            DateTime.Now,
            type,
            amount,
            balance);
    }
}
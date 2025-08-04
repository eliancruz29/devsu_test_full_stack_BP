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
        int balance,
        Status status
    ) : base(id)
    {
        AccountId = accountId;
        Date = date;
        Type = type;
        Amount = amount;
        Balance = balance;
        Status = status;
    }

    public Guid AccountId { get; private set; }

    public DateTime Date { get; private set; }

    public TransferTypes Type { get; private set; }

    public int Amount { get; private set; }

    public int Balance { get; private set; }

    public Status Status { get; private set; }

    public static Transfer Create(
        Guid accountId,
        TransferTypes type,
        int amount,
        int balance)
    {
        return new(
            Guid.NewGuid(),
            accountId,
            DateTime.Now,
            type,
            amount,
            balance,
            Status.Active);
    }

    public void Update(
        TransferTypes type,
        int amount)
    {
        Type = type;
        Amount = amount;
    }
}

using DevsuApi.Domain.Enums;
using DevsuApi.SharedKernel;

namespace DevsuApi.Domain.Entities;

public sealed class Account : BaseEntity
{
    private readonly List<Transfer> _transfers = [];

    private Account(
        Guid id,
        Guid clientId,
        string accountNumber,
        AccountTypes type,
        int openingBalance,
        Status status
    ) : base(id)
    {
        ClientId = clientId;
        AccountNumber = accountNumber;
        Type = type;
        OpeningBalance = openingBalance;
        Status = status;
    }

    public Guid ClientId { get; private set; }

    public string AccountNumber { get; private set; } = string.Empty;

    public AccountTypes Type { get; private set; }

    public int OpeningBalance { get; private set; }

    public Status Status { get; private set; }

    public IReadOnlyCollection<Transfer> Transfers => _transfers;
    
    public static Account Create(
        Guid id,
        Guid clientId,
        string accountNumber,
        AccountTypes type,
        int openingBalance,
        Status status)
    {
        return new (
            id,
            clientId,
            accountNumber,
            type,
            openingBalance,
            status);
    }
}
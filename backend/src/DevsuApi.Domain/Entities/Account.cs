using DevsuApi.Domain.Enums;
using DevsuApi.Domain.Exceptions.Accounts;
using DevsuApi.Domain.Exceptions.Transfers;
using DevsuApi.SharedKernel;

namespace DevsuApi.Domain.Entities;

public sealed class Account : BaseEntity
{
    private readonly int DAILY_DEBIT_LIMIT = 1000;
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
        Guid clientId,
        string accountNumber,
        AccountTypes type,
        int openingBalance)
    {
        return new(
            Guid.NewGuid(),
            clientId,
            accountNumber,
            type,
            openingBalance,
            Status.Active);
    }

    public void Update(
        string accountNumber,
        AccountTypes type,
        int openingBalance,
        Status status)
    {
        AccountNumber = accountNumber;
        Type = type;
        OpeningBalance = openingBalance;
        Status = status;
    }

    public Transfer AddTransfer(
        TransferTypes type,
        int amount)
    {
        int balance = GetBalance();
        if (balance <= 0 && type == TransferTypes.Debit)
        {
            throw new AccountUnAvailableBalanceException(Id);
        }

        int dailyDebit = GetDailyDebitTotal();
        if (dailyDebit > DAILY_DEBIT_LIMIT && type == TransferTypes.Debit)
        {
            throw new TransferMaxDailyLimitReachedException(Id, DAILY_DEBIT_LIMIT);
        }

        Transfer newTransfer = Transfer.Create(
            Id,
            type,
            amount,
            GetBalance());

        _transfers.Add(newTransfer);

        return newTransfer;
    }

    private int GetBalance()
    {
        return OpeningBalance + _transfers.Sum(t => t.Type == TransferTypes.Credit ? t.Amount : -t.Amount);
    }

    private int GetDailyDebitTotal()
    {
        return _transfers
            .Where(t => t.Type == TransferTypes.Debit && t.Date.Date == DateTime.Now.Date)
            .Sum(t => t.Amount);
    }
}

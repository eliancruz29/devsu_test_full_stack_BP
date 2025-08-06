using DevsuApi.Domain.Enums;

namespace DevsuApi.Domain.Entities;

public abstract class TransfersReport
{
    protected TransfersReport(
        DateTime date,
        string clientName,
        string accountNumber,
        AccountTypes type,
        int openingBalance,
        int amount,
        int balance,
        Status status)
    {
        Date = date;
        ClientName = clientName;
        AccountNumber = accountNumber;
        Type = type;
        OpeningBalance = openingBalance;
        Amount = amount;
        Balance = balance;
        Status = status;
    }

    public DateTime Date { get; private set; }
    public string ClientName { get; private set; } = string.Empty;
    public string AccountNumber { get; private set; } = string.Empty;
    public AccountTypes Type { get; private set; }
    public int OpeningBalance { get; private set; }
    public int Amount { get; private set; }
    public int Balance { get; private set; }
    public Status Status { get; private set; }
}

using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Enums;
using DevsuApi.Infrastructure.Extensions;

namespace DevsuApi.Features.Reports.GetTransfersReport;

public class TransfersReportResponse
{
    public DateTime Date { get; private set; }
    public string ClientName { get; private set; } = string.Empty;
    public string AccountNumber { get; private set; } = string.Empty;
    public AccountTypes Type { get; private set; }
    public string TypeName => Type.GetDatabaseName();
    public int OpeningBalance { get; private set; }
    public int Amount { get; private set; }
    public int Balance { get; private set; }
    public Status Status { get; private set; }
    public string StatusName => Status.GetDatabaseName();

    public static TransfersReportResponse Create(Client c, Account a, Transfer t)
    {
        bool isDebit = TransferTypes.Debit == t.Type;

        return new()
        {
            Date = t.Date,
            ClientName = c.Name,
            AccountNumber = a.AccountNumber,
            Type = a.Type,
            OpeningBalance = a.OpeningBalance,
            Amount = t.Amount * (isDebit ? -1 : 1),
            Balance = t.Balance,
            Status = t.Status
        };
    }
}

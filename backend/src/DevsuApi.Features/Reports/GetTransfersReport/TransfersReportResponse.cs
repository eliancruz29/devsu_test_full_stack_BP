using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Enums;
using DevsuApi.Infrastructure.Extensions;

namespace DevsuApi.Features.Reports.GetTransfersReport;

public class TransfersReportResponse : TransfersReport
{
    private TransfersReportResponse(
        DateTime date,
        string clientName,
        string accountNumber,
        AccountTypes type,
        int openingBalance,
        int amount,
        int balance,
        Status status
    ) : base(
        date,
        clientName,
        accountNumber,
        type,
        openingBalance,
        amount,
        balance,
        status)
    { }

    public string TypeName => Type.GetDatabaseName();
    public string StatusName => Status.GetDatabaseName();

    public static TransfersReportResponse Create(Client c, Account a, Transfer t)
    {
        bool isDebit = TransferTypes.Debit == t.Type;

        return new(
            t.Date,
            c.Name,
            a.AccountNumber,
            a.Type,
            a.OpeningBalance,
            t.Amount * (isDebit ? -1 : 1),
            t.Balance,
            t.Status);
    }
}

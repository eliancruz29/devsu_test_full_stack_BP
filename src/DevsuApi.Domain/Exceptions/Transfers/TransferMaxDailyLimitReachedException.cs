namespace DevsuApi.Domain.Exceptions.Transfers;

public sealed class TransferMaxDailyLimitReachedException : Exception
{
    public TransferMaxDailyLimitReachedException(Guid accountId, int dailyLimit)
        : base($"The account with the ID = {accountId} has reached it's daily limit of Debits = {dailyLimit}.")
    {
    }
}

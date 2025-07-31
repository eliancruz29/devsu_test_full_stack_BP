namespace DevsuApi.Domain.Exceptions.Transfers;

public sealed class TransferMaxDailyLimitReachedException(Guid accountId, int dailyLimit)
    : Exception($"The account with the ID = {accountId} has reached it's daily limit of Debits = {dailyLimit}.")
{ }

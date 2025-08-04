namespace DevsuApi.Domain.Exceptions.Transfers;

public sealed class TransferMaxDailyLimitReachedException(Guid accountId, int dailyLimit)
    : Exception($"La cuenta con el ID = {accountId} ha alcanzado el limite de debito diario = {dailyLimit}.")
{ }

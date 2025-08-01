namespace DevsuApi.Domain.Exceptions.Accounts;

public sealed class AccountUnAvailableBalanceException(Guid id)
    : Exception($"The account with the ID = {id} has not available Balance")
{ }

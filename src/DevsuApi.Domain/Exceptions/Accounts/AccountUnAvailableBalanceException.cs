namespace DevsuApi.Domain.Exceptions.Accounts;

public sealed class AccountUnAvailableBalanceException : Exception
{
    public AccountUnAvailableBalanceException(Guid id)
        : base($"The account with the ID = {id} has not available Balance")
    {
    }
}

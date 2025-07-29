namespace DevsuApi.Domain.Exceptions.Accounts;

public sealed class AccountNotFoundException : Exception
{
    public AccountNotFoundException(Guid id)
        : base($"The account with the ID = {id} was not found")
    {
    }
}

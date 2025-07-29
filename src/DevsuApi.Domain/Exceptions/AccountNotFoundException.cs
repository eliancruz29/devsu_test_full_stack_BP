namespace DevsuApi.Domain.Exceptions;

public sealed class AccountNotFoundException : Exception
{
    public AccountNotFoundException(Guid id)
        : base($"The account with the ID = {id} was not found")
    {
    }
}

namespace DevsuApi.Domain.Exceptions.Clients;

public sealed class AccountAlreadyExistsException : Exception
{
    public AccountAlreadyExistsException(Guid clientId, string accountNumber)
        : base($"The client with the ID = {clientId} already has a Account = {accountNumber}.")
    {
    }
}

namespace DevsuApi.Domain.Exceptions.Clients;

public sealed class AccountAlreadyExistsException(Guid clientId, string accountNumber)
    : Exception($"The client with the ID = {clientId} already has a Account = {accountNumber}.")
{ }

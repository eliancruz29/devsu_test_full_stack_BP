namespace DevsuApi.Domain.Exceptions.Clients;

public sealed class AccountAlreadyExistsException(Guid clientId, string accountNumber)
    : Exception($"El client con el ID = {clientId} ya tiene una Cuenta = {accountNumber}.")
{ }

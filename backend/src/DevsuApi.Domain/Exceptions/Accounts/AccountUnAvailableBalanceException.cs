namespace DevsuApi.Domain.Exceptions.Accounts;

public sealed class AccountUnAvailableBalanceException(Guid id)
    : Exception($"La cuenta con el ID = {id} no tiene balance disponible.")
{ }

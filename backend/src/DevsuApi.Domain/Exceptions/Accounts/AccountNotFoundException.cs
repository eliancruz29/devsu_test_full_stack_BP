using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Exceptions.Shared;

namespace DevsuApi.Domain.Exceptions.Accounts;

public sealed class AccountNotFoundException(Guid id) : NotFoundBaseException(nameof(Account), id) { }

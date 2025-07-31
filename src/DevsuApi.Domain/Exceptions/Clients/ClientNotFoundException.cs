using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Exceptions.Shared;

namespace DevsuApi.Domain.Exceptions.Clients;

public sealed class ClientNotFoundException(Guid id) : NotFoundBaseException(nameof(Client), id) { }

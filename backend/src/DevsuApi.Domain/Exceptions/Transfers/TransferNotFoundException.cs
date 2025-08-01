using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Exceptions.Shared;

namespace DevsuApi.Domain.Exceptions.Transfers;

public sealed class TransferNotFoundException(Guid id) : NotFoundBaseException(nameof(Transfer), id) { }

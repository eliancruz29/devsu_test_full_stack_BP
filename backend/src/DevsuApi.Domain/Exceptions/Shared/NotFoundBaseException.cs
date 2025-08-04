namespace DevsuApi.Domain.Exceptions.Shared;

public abstract class NotFoundBaseException(string modelType, Guid id) : Exception($"El modelo {modelType} con el ID = {id} no existe.") { }

namespace DevsuApi.Domain.Exceptions.Shared;

public abstract class NotFoundBaseException(string modelType, Guid id) : Exception($"The {modelType} with the ID = {id} was not found") { }

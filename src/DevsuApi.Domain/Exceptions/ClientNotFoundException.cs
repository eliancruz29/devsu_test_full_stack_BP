namespace DevsuApi.Domain.Exceptions;

public sealed class ClientNotFoundException : Exception
{
    public ClientNotFoundException(Guid id)
        : base($"The client with the ID = {id} was not found")
    {
    }
}

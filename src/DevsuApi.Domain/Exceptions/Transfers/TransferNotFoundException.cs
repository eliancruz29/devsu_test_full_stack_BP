namespace DevsuApi.Domain.Exceptions.Transfers;

public sealed class TransferNotFoundException : Exception
{
    public TransferNotFoundException(Guid id)
        : base($"The transfer with the ID = {id} was not found")
    {
    }
}

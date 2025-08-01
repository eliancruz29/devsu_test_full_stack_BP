namespace DevsuApi.SharedKernel;

public abstract class BaseEntity
{
    private readonly List<DomainEvent> _domainEvents = new();

    protected BaseEntity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private init; }

    public IReadOnlyCollection<DomainEvent> GetDomainEvents() => _domainEvents;

    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
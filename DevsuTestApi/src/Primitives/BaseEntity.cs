namespace DevsuTestApi.Primitives;

public abstract class BaseEntity
{
    protected BaseEntity(Guid id)
    {
        Id = id;
    }
    
    public Guid Id { get; private init; }
}
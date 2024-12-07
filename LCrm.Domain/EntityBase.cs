namespace LCrm.Domain;

public abstract class EntityBase
{
    protected EntityBase(Guid id, DateTime createdOn)
    {
        Id = id;
        CreatedOn = createdOn;
    }

    public Guid Id { get; private set; }
    public DateTime CreatedOn { get; private set; }
}
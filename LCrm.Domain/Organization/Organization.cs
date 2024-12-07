namespace LCrm.Domain.Organization;

public class Organization : EntityBase
{
    public string Name { get; private set; }
    
    public Organization(Guid id, DateTime createdOn, string name) : base(id, createdOn)
    {
        Name = name;
    }
}
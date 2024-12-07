namespace LCrm.Domain.Organization;

public class User : EntityBase
{
    public User(Guid id, DateTime createdOn, string name, Guid organizationId) : base(id, createdOn)
    {
        Name = name;
        OrganizationId = organizationId;
    }

    public string Name { get; set; }

    public Guid OrganizationId { get; private set; }
}
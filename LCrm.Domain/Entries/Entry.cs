using LCrm.Domain.Entries.Events;

namespace LCrm.Domain.Entries;

public class Entry : AggregateRoot
{
    public string Name { get; private set; } = "Untitled";
    public EntryStatus Status { get; private set; } = EntryStatus.Ready;
    
    public void Apply(EntryCreated @event)
    {
        Name = @event.Name;
    }
    
    public void Apply(EntryStatusUpdated @event)
    {
        Status = @event.Status;
    }
}
using LCrm.Domain.Entries;

namespace LCrm.Domain;

public interface INotificationService
{
    void PublishEvent(Guid aggregateId, object @event);
    void PublishEntry(Guid aggregateId, Entry entry);
}
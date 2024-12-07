using LCrm.Domain;
using LCrm.Domain.Entries;
using LCrm.Web.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace LCrm.Web.Services;

public class NotificationService(IHubContext<EventHub> hubContext) : INotificationService
{
    public void PublishEvent(Guid aggregateId, object @event)
    {
        hubContext.Clients.Group(aggregateId.ToString())
            .SendAsync("PublishEvent", aggregateId, @event, @event.GetType().Name);
    }

    public void PublishEntry(Guid aggregateId, Entry entry)
    {
        hubContext.Clients.Group("entries")
            .SendAsync("PublishEntry", aggregateId, entry);
    }
}
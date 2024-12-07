using Microsoft.AspNetCore.SignalR;

namespace LCrm.Web.Hubs;

public class EventHub : Hub
{
    // public async Task PublishEvent(Guid aggregateId, object @event)
    // {
    //     await Clients.Group(aggregateId.ToString())
    //         .SendAsync("PublishEvent", aggregateId, @event);
    // }

    public async Task SubscribeToAggregate(Guid aggregateId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, aggregateId.ToString());
    }
    
    public async Task SubscribeToEntries()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "entries");
    }
}
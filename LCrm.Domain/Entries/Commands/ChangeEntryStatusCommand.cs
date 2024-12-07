using LCrm.Domain.Entries.Events;
using MediatR;

namespace LCrm.Domain.Entries.Commands;

public record ChangeEntryStatusCommand(Guid Id, EntryStatus Status) : IRequest;

public class ChangeEntryStatusCommandHandler(IEventStore eventStore, INotificationService notificationService) : IRequestHandler<ChangeEntryStatusCommand>
{
    public Task Handle(ChangeEntryStatusCommand request, CancellationToken cancellationToken)
    {
        var stream = new EventStream<Entry>(eventStore, request.Id);
        _ = stream.GetEntity(); // Ensures sequence number is updated
        stream.Append(new EntryStatusUpdated(request.Status));
        eventStore.SaveChanges();
        notificationService.PublishEntry(request.Id, stream.GetEntity());
        return Task.CompletedTask;
    }
}
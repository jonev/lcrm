using LCrm.Domain.Entries.Events;
using MediatR;

namespace LCrm.Domain.Entries.Commands;

public record CreateEntryCommand(Guid Id, string Name) : IRequest;

public class CreateEntryCommandHandler(IEventStore eventStore, INotificationService notificationService) : IRequestHandler<CreateEntryCommand>
{
    public Task Handle(CreateEntryCommand request, CancellationToken cancellationToken)
    {
        var stream = new EventStream<Entry>(eventStore, request.Id);
        _ = stream.GetEntity(); // Ensures sequence number is updated
        stream.Append(new EntryCreated(request.Name));
        eventStore.SaveChanges();
        notificationService.PublishEntry(request.Id, stream.GetEntity());
        return Task.CompletedTask;
    }
}
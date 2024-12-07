using LCrm.Domain.Entries;
using LCrm.Domain.Entries.Events;
using MediatR;

namespace LCrm.Domain.Tests.Entries;

public abstract class EntryTest<TCommand> : CommandHandlerTest<TCommand> where TCommand : IRequest
{
    protected Guid Id => _aggregateId;
    
    // Events
    protected EntryCreated Entry_created_with_name(string name)
    {
        return new EntryCreated(name);
    }
    
    protected EntryStatusUpdated Entry_status_updated(EntryStatus status)
    {
        return new EntryStatusUpdated(status);
    }
}
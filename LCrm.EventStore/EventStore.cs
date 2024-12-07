using System.Data;
using Dapper;
using LCrm.Domain;

namespace LCrm.EventStore;

public class EventStore(
    IDbConnection connection,
    INotificationService notificationService) 
    : IEventStore
{
    public IEnumerable<StoredEvent> GetEvents(Guid aggregateId)
    {   
        const string query = """
                             SELECT "aggregateid", "sequencenumber", "timestamp", 
                                    "eventtypename", "eventbody", "rowversion"
                             FROM "events"
                             WHERE "aggregateid" = @AggregateId
                             ORDER BY "sequencenumber";
                             """;


        return connection.Query<DatabaseEvent>(
                query,
                new { AggregateId = aggregateId })
            .Select(e => e.ToStoredEvent());
    }

    public IEnumerable<StoredEvent> GetEventsUntilSequence(Guid aggregateId, int sequence)
    {
        const string query = """
                             SELECT "aggregateid", "sequencenumber", "timestamp", 
                                    "eventypename", "eventbody", "rowversion"
                             FROM "events"
                             WHERE "aggregateid" = $1
                               AND "sequencenumber" <= $2
                             ORDER BY "sequencenumber";
                             """;

        return connection.Query<DatabaseEvent>(
                query,
                new { AggregateId = aggregateId, Sequence = sequence })
            .Select(e => e.ToStoredEvent());
    }

    private readonly List<StoredEvent> _newEvents = [];
    
    public void AppendEvent(StoredEvent @event)
    {
        _newEvents.Add(@event);
    }

    public void SaveChanges()
    {
        const string insertCommand = """
                                     INSERT INTO "events" 
                                         ("aggregateid", "sequencenumber", "timestamp", 
                                          "eventtypename", "eventbody") 
                                     VALUES 
                                         (@AggregateId, @SequenceNumber, @Timestamp, @EventTypeName, @EventBody);
                                     """;

        connection.Open();
        using var transaction = connection.BeginTransaction();

        connection.Execute(
            insertCommand,
            _newEvents.Select(DatabaseEvent.FromStoredEvent),
            transaction);
        
        transaction.Commit();

        foreach (var storedEvent in _newEvents)
        {
            notificationService.PublishEvent(
                storedEvent.AggregateId,
                storedEvent.EventData);
        }
        
        _newEvents.Clear();
    }
}
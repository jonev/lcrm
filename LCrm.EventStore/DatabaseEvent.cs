using System.Reflection;
using System.Text.Json;
using LCrm.Domain;

namespace LCrm.EventStore;

public record DatabaseEvent {
    public Guid AggregateId { get; set; }
    public int SequenceNumber { get; set; }
    public DateTime Timestamp { get; set; }
    public string? EventTypeName { get; set; }
    public string? EventBody { get; set; }
    public Int64 RowVersion { get; set; }

    public static DatabaseEvent FromStoredEvent(StoredEvent storedEvent)
    {
        var typeName = storedEvent.EventData.GetType().FullName;

        if (typeName == null)
            throw new Exception("Could not get type name from EventData");

        return new DatabaseEvent{
            AggregateId = storedEvent.AggregateId,
            SequenceNumber = storedEvent.SequenceNumber,
            Timestamp = storedEvent.Timestamp,
            EventTypeName = typeName,
            EventBody = JsonSerializer.Serialize(storedEvent.EventData)
        };
    }

    public StoredEvent ToStoredEvent()
    {
        if (EventTypeName == null)
            throw new Exception("EventTypeName should not be null");
        if (EventBody == null)
            throw new Exception("EventBody should not be null");

        var eventType = DomainAssembly.GetType(EventTypeName);
        if (eventType == null)
            throw new Exception($"Type not Found: {EventTypeName}");
        
        var eventData = JsonSerializer.Deserialize(EventBody, eventType);
        if (eventData == null)
            throw new Exception($"Could not deserialize EventBody as {EventTypeName}");

        return new StoredEvent(
            AggregateId, 
            SequenceNumber, 
            Timestamp, 
            eventData);
    }
    
    private static readonly Assembly DomainAssembly = typeof(StoredEvent).Assembly;
}
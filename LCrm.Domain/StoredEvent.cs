namespace LCrm.Domain;

public record StoredEvent(
    Guid AggregateId,
    int SequenceNumber,
    DateTime Timestamp,
    object EventData
);
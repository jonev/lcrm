CREATE TABLE Events (
    AggregateId UUID NOT NULL,
    SequenceNumber INT NOT NULL,
    Timestamp TIMESTAMP(7) WITHOUT TIME ZONE NOT NULL,
    EventTypeName VARCHAR(256) NOT NULL,
    EventBody TEXT NOT NULL,
    RowVersion BIGINT NOT NULL DEFAULT 0,
    PRIMARY KEY (AggregateId, SequenceNumber)
);

CREATE OR REPLACE FUNCTION increment_version()
    RETURNS TRIGGER AS $$
BEGIN
    NEW.Version := OLD.Version + 1;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER update_version
    BEFORE UPDATE ON Events
    FOR EACH ROW
EXECUTE FUNCTION increment_version();
namespace LCrm.Domain;

public abstract class AggregateRoot
{
    public void Apply(object @event){}
}
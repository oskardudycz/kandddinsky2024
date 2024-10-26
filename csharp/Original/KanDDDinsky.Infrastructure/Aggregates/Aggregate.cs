using KanDDDinsky.Core.Events;

namespace KanDDDinsky.Core.Aggregates;

public abstract class Aggregate<TKey>(TKey id)
{
    public TKey Id { get; } = id;

    private readonly List<IDomainEvent> domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> DomainEvents
        => domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent) =>
        domainEvents.Add(domainEvent);

    public void ClearEvents() =>
        domainEvents.Clear();
}

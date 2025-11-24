using System;

namespace Domain.Common;

public abstract class BaseDomain : DomainResult
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    protected void AddDomainEvent(IDomainEvent eventItem) => _domainEvents.Add(eventItem);
    public void RemoveDomainEvent(IDomainEvent eventItem) => _domainEvents.Remove(eventItem);
    public void ClearDomainEvents() => _domainEvents.Clear();
}

public abstract class BaseDomain<T> : DomainResult<T>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    protected void AddDomainEvent(IDomainEvent eventItem) => _domainEvents.Add(eventItem);
    public void RemoveDomainEvent(IDomainEvent eventItem) => _domainEvents.Remove(eventItem);
    public void ClearDomainEvents() => _domainEvents.Clear();
}

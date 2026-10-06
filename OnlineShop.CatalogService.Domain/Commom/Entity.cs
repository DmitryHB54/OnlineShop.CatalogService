namespace OnlineShop.CatalogService.Domain.Commom;
public class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    private readonly List<IDomainEvent> _domainEvent = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvent.AsReadOnly();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvent.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvent.Clear();
}
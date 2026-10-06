using MediatR;
using OnlineShop.CatalogService.Domain.Commom;

namespace OnlineShop.CatalogService.Application.Common;
public sealed class DomainEventNotification<TDomainEvent> : INotification
    where TDomainEvent : IDomainEvent
{
    public TDomainEvent DomainEvent { get; }
    public DomainEventNotification(TDomainEvent domainEvent) => DomainEvent = domainEvent;
}
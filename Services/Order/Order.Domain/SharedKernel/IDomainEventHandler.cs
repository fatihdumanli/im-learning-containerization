using MediatR;

namespace Ordering.Domain.SharedKernel
{
    public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent> where TDomainEvent: IDomainEvent
    {
    }
}
using EventBus.Events;

namespace Ordering.API.Application.IntegrationEvents.Events
{
     public class OrderShippedIntegrationEvent : IntegrationEvent
     {
         public int OrderId { get; private set; }
         
         public OrderShippedIntegrationEvent(int orderId)
         {
             this.OrderId = orderId;
         }
     }
}
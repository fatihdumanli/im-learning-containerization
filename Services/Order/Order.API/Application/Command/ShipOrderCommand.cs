using MediatR;

namespace Ordering.API.Application.Command
{
    public class ShipOrderCommand : IRequest<bool>
    {
        public int OrderId { get; private set; }

        public ShipOrderCommand(int orderId)
        {
            this.OrderId = orderId;
        }        
    }
}
using MediatR;

namespace Ordering.API.Application.Command
{
    public class SetOrderStatusToPaidCommand : IRequest<bool>
    {
        public int OrderId { get; private set; }

        public SetOrderStatusToPaidCommand(int orderId)
        {
            this.OrderId = orderId;
        }
        
    }
}
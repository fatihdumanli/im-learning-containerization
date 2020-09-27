using MediatR;

namespace Ordering.API.Application.Command
{
    public class SetOrderStatusToStockConfirmedCommand : IRequest<bool>
    {
        public int OrderId { get; private set; }
    
        public SetOrderStatusToStockConfirmedCommand(int orderId)
        {
            this.OrderId = orderId;            
        }
    }
}
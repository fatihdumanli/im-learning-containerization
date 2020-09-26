using MediatR;

namespace Ordering.API.Application.Command
{
    public class SetOrderStatusAwaitingStockValidationCommand : IRequest<bool>
    {
        public int OrderId { get; private set; }

        public SetOrderStatusAwaitingStockValidationCommand(int orderId)
        {
            this.OrderId = orderId;            
        }
    }
}
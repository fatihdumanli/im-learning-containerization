using MediatR;

namespace Ordering.API.Application.Command
{
    public class SetOrderStatusToStockRejectedCommand : IRequest<bool>
    {
        public int OrderId { get; private set; }

        public SetOrderStatusToStockRejectedCommand(int orderId)
        {
            this.OrderId = orderId;
        }
    }
}
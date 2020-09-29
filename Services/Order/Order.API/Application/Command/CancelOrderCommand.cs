using MediatR;

namespace Ordering.API.Application.Command
{
    public class CancelOrderCommand : IRequest<bool>
    {
        public int OrderId { get; private set; }
        public string CancellationReason { get; private set; }

        public CancelOrderCommand(int orderId, string reason)
        {
            this.OrderId = orderId;
            this.CancellationReason = reason;            
        }
    }
}
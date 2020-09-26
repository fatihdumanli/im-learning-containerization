using MediatR;

namespace Ordering.API.Application.Command
{
     /*
        SET ORDER STATUS AWAITING STOCK VALIDATION COMMAND
        TRIGGER: GRACE PERIOD CONFIRMED INTEGRATION EVENT (BILLING)
    */
    public class SetOrderStatusAwaitingStockValidationCommand : IRequest<bool>
    {
        public int OrderId { get; private set; }

        public SetOrderStatusAwaitingStockValidationCommand(int orderId)
        {
            this.OrderId = orderId;            
        }
    }
}
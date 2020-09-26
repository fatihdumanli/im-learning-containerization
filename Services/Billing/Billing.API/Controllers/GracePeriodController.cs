using Billing.API.IntegrationEvents.Events;
using EventBus.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Billing.API
{

    [Route("api/[controller]")]
    [ApiController]
    public class GracePeriodController : ControllerBase
    {
        private IEventBus _eventBus;

        public GracePeriodController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [Route("confirm")]
        [HttpPost]
        public IActionResult ConfirmGracePeriodForOrder(int orderId)
        {
            if(orderId == 0)
            {
                return BadRequest();
            }

            var gracePeriodConfirmedForOrderEvent = new GracePeriodConfirmedForOrderIntegrationEvent(orderId);
            
            _eventBus.Publish(gracePeriodConfirmedForOrderEvent);

            return Ok();
        }

    }
    
}
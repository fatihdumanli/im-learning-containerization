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
        public Task<IActionResult> ConfirmGracePeriodForOrder(int orderId)
        {
            _eventBus.Publish(null);
            return null;
        }

    }
    
}
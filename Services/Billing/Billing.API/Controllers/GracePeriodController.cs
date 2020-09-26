using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Billing.API
{

    [Route("api/[controller]")]
    [ApiController]
    public class GracePeriodController : ControllerBase
    {

        [Route("confirm")]
        [HttpPost]
        public Task<IActionResult> ConfirmGracePeriodForOrder(int orderId)
        {
            return null;
        }

    }
    
}
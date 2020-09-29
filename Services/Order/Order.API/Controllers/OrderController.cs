using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.Command;

namespace Ordering.API.Application.Controllers
{
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
         private IMediator _mediator;
         private ILogger<OrderController> _logger;

         public OrderController(IMediator mediator, ILogger<OrderController> logger)
         {
             this._logger = logger;
             this._mediator = mediator;             
         }


        [HttpPost]
        [Route("ship")]
         public async Task<IActionResult> ShipOrderAsync(int orderId)
         {
             var command = new ShipOrderCommand(orderId);

             _logger.LogInformation(" [x] OrderController.ShipOrderAsync(): Sending ShipOrderCommand for order with id {0}", orderId);
             await _mediator.Send(command);

             return Ok();
         }              
    }
}
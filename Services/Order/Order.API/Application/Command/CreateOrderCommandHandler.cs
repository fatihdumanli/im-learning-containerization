using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Order.API.Application.Command
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private ILogger<CreateOrderCommandHandler> _logger;
        public CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger)
        {
            this._logger = logger;
        }
        /*
            1. Instantiate new Address valueObject
            2. Instantiate new Order aggregate root.
        */
        public Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            //In request,
            //--OrderItems.
            //--General information about order, and address


            
            _logger.LogInformation("------- [.] Create Order Command Handler...");
            _logger.LogInformation("REQUEST --> " + JsonConvert.SerializeObject(request));

            
            return Task.FromResult(true);
        }
    }
}
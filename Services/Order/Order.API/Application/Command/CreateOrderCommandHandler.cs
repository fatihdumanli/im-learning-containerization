using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Order.API.Application.Command
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private ILogger<CreateOrderCommandHandler> _logger;
        public CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger)
        {
            this._logger = logger;
        }

        public Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("------- [.] Create Order Command Handler...");
            return Task.FromResult(true);
        }
    }
}
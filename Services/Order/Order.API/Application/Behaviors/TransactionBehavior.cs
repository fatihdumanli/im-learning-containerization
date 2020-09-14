using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
        
        public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            this._logger = logger;            
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetType().Name;
            response = await next();

            _logger.LogInformation(string.Format("----- [.] TransactionBehavior: Begin transaction for command {0}", typeName));
            
            return response;
        }
    }
}
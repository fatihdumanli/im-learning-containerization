using Microsoft.Extensions.Logging;

namespace Ordering.API.Infrastructure.Logging
{
    //DEPENDENT TO ASP.NET CORE LOGGER  
    public class Loggable
    {        
        protected ILogger<Loggable> _logger;
        public Loggable(ILogger<Loggable> logger)
        {
            _logger = logger;
        }

        protected Loggable()
        {   
            var x = 2;
        }
    }
}
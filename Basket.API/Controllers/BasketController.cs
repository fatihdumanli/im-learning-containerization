using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Basket.API.Model;
using Basket.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IIdentityService _identityService;
        //private readonly IEventBus _eventBus;
        private ILogger<BasketController> _logger;

        public BasketController(IBasketRepository repository,
            ILogger<BasketController> logger,
            IIdentityService identityService)
        {
            _repository = repository;
            _logger = logger;
            _identityService = identityService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync(string id)
        {
            var basket = await _repository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }
    }
}

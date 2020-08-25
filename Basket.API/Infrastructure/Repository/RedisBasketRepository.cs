using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.API.Model;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Basket.API.Infrastructure.Repository 
{
    public class RedisBasketRepository : IBasketRepository
    {
        private ConnectionMultiplexer _redis;
        private IDatabase _database;


        public RedisBasketRepository(ConnectionMultiplexer redis)
        {
            this._redis = redis;
            this._database = redis.GetDatabase();   
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {     
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket> GetBasketAsync(string customerId)
        {
            var data = await _database.StringGetAsync(customerId);

            if(data.IsNullOrEmpty)
            {
                return null;
            } 

            return JsonConvert.DeserializeObject<CustomerBasket>(data);
        }

        public IEnumerable<string> GetUsers()
        {
            var server = this.GetServer();
            var data = server.Keys();

            return data?.Select(k => k.ToString());
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.BuyerId,
                JsonConvert.SerializeObject(basket));

            if(!created) {
                //todo log
                return null;
            }

            //todo success log
            return await this.GetBasketAsync(basket.BuyerId);
        }

        private IServer GetServer() {
            var endPoints = _redis.GetEndPoints();
            return _redis.GetServer(endPoints.First());            
        }
    }

}
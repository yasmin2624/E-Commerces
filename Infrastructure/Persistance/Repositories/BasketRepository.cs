using Domain.Contracts;
using Domain.Entities.BasketModules;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase database = connection.GetDatabase(); 
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdates = await database.StringSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(1));
            if (IsCreatedOrUpdates)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                return null;
            }
        }

        public async Task<CustomerBasket?> GetBasketAsync(string Key)
        {
          var Basket = await database.StringGetAsync(Key);
            if (Basket.IsNullOrEmpty)
            {
                return null;
            }
            else
            {
                return JsonSerializer.Deserialize<CustomerBasket>(Basket);
            }
        }


        public async Task<bool> DeleteBasketAsync(string Id) 
            =>await database.KeyDeleteAsync(Id);
        
    }
}

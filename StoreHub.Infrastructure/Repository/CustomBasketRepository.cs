using Domain.Contracts;
using Domain.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace StoreHub.Infrastructure.Repository
{
    public class CustomBasketRepository(IConnectionMultiplexer connection) : ICustomBasketRepository
    {
        private readonly IDatabase dataBase = connection.GetDatabase();

        public async Task<CustomBasket> GetBasketAsync(string id)
        {
            var redisValue = await dataBase.StringGetAsync(id);
            if (redisValue.IsNullOrEmpty) return null;
            var basket = JsonSerializer.Deserialize<CustomBasket>(redisValue);
            if (basket is null) return null;
            return basket;
        }

        public async Task<CustomBasket> UpdateBasketAsync(CustomBasket model, TimeSpan? timeToLive = null)
        {
            var redisValue = JsonSerializer.Serialize(model);
            var flag = await dataBase.StringSetAsync(model.Id, redisValue, TimeSpan.FromDays(30));
            return flag ? await GetBasketAsync(model.Id) : null!;
        }

        public async Task<bool> DeleteBasket(string id)
        {
            return await dataBase.KeyDeleteAsync(id);
        }
    }
}

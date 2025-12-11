using StackExchange.Redis;
using StoreHub.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace StoreHub.Infrastructure.Repository
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private IDatabase dataBase = connection.GetDatabase();
        public async Task<string> GetKey(string Key)
        {
            var value = await dataBase.StringGetAsync(Key);
            return value.IsNullOrEmpty ? default : value;
        }

        public Task SetKey(string key, object value, TimeSpan duration)
        {
            var redisValue = JsonSerializer.Serialize(value);
            return dataBase.StringSetAsync(key, redisValue, duration);
        }
    }
}

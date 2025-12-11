using StoreHub.Application.Services.Contracts;
using StoreHub.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Application.Services
{
    public class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        public async Task<string> GetKey(string id)
        {
            return await cacheRepository.GetKey(id);
        }

        public Task SetKey(string id, object value, TimeSpan duration)
        {
            return cacheRepository.SetKey(id, value, duration);
        }
    }
}

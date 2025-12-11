using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Application.Services.Contracts
{
    public interface ICacheService
    {
        Task<string> GetKey(string id);
        Task SetKey(string id, object value, TimeSpan duration);
    }
}

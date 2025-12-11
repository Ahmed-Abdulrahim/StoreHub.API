using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Core.Contracts
{
    public interface ICacheRepository
    {
        Task SetKey(string key, object value, TimeSpan duration);
        Task<string> GetKey(string Key);
    }
}

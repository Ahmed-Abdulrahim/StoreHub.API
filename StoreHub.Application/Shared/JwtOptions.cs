using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Application.Shared
{
    public class JwtOptions
    {
        public string issuer { get; set; }
        public string audience { get; set; }
        public string secretKey { get; set; }

    }
}

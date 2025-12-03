using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ProductRequestDto
    {

        public int? BrandId { get; set; }
        public int? typeId { get; set; }
        public int? pageIndex { get; set; } = 1;
        public int? pageSize { get; set; } = 5;
        public string? Sort { get; set; }
        public string? Search { get; set; }

    }
}

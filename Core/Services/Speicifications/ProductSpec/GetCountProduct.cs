using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Speicifications.ProductSpec
{
    public class GetCountProduct : BaseSpeicification<Product, int>
    {
        public GetCountProduct(int? brandId, int? typeId) : base
            (
            p =>
            //tring.IsNullOrEmpty(search) || p.Name.ToLower().Contains(search)) &&
            (!brandId.HasValue || p.BrandId == brandId)
            &&
            (!typeId.HasValue || p.TypeId == typeId)
            )
        {

        }
    }
}

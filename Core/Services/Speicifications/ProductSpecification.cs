using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Speicifications
{
    public class ProductSpecification : BaseSpeicification<Product, int>
    {
        public ProductSpecification(int? brandId, int? typeId, string? sort, int? pageIndex, int? pageSize) : base
            (
            p =>
            (!brandId.HasValue || p.BrandId == brandId)
            &&
            (!typeId.HasValue || p.TypeId == typeId)
            )
        {
            AddIncludes();

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "nameasc":
                        ApplyOrderBy(p => p.Name);
                        break;
                    case "namedesc":
                        ApplyOrderByDescending(p => p.Name);
                        break;
                    case "priceasc":
                        ApplyOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        ApplyOrderByDescending(p => p.Price);
                        break;
                    default:
                        ApplyOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                ApplyOrderBy(p => p.Name);
            }

            ApplyPagination(pageSize, pageIndex);
        }
        public ProductSpecification(int id) : base(p => p.Id == id)
        {
            AddIncludes();
        }
        public ProductSpecification(Expression<Func<Product, bool>> critria) : base(critria)
        {
            AddIncludes();
        }
        void AddIncludes()
        {
            GetInclude(p => p.ProductBrands);
            GetInclude(p => p.ProductTypes);
        }
    }
}

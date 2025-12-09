using Domain.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Speicifications.ProductSpec
{
    public class ProductSpecification : BaseSpeicification<Product, int>
    {

        public ProductSpecification() : base(null)
        {
            AddIncludes();
        }
        public ProductSpecification(ProductRequestDto model) : base
            (
            p =>
            (string.IsNullOrEmpty(model.Search) || p.Name.ToLower().Contains(model.Search)) &&
            (!model.BrandId.HasValue || p.BrandId == model.BrandId)
            &&
            (!model.typeId.HasValue || p.TypeId == model.typeId)
            )
        {
            AddIncludes();

            if (!string.IsNullOrEmpty(model.Sort))
            {
                switch (model.Sort.ToLower())
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

            ApplyPagination(model.pageSize, model.pageIndex);
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

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
        public ProductSpecification() : base(null)
        {
            AddIncludes();
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

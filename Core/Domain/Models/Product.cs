using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product :BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        [ForeignKey(nameof(ProductBrands))]
        public int BrandId { get; set; }
        public virtual ProductBrand ProductBrands { get; set; }
        [ForeignKey(nameof(ProductTypes))]
        public int TypeId { get; set; }
        public virtual ProductType ProductTypes { get; set; }

    }
}
